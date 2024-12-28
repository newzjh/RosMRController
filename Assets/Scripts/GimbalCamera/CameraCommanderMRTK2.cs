using System;
using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class CameraCommander : MonoBehaviour
{
    [SerializeField]
    private string cameraIPStr = "192.168.8.101";

    [SerializeField]
    private int cameraPort = 9554;

    [SerializeField]
    private float maxSpeed = 10;

    [SerializeField]
    private float thresholdAngle = 8;
    private IPAddress cameraIP;
    private HeadTracker headTracker;

    private byte[] packetData = new byte[44]
    {
        0xfb, // 同步码
        0x2c,
        0x00, // 常规指令码
        0x00, // 参数1
        0x00,
        0x00, // 参数2
        0x00,
        0x00, // 飞控遥测参数，叠加显示 // 俯仰角
        0x00,
        0x00, // 滚转角
        0x00,
        0x00, // 航向角
        0x00,
        0x18, // GPS年月日时分秒
        0x08,
        0x0d,
        0x10,
        0x27,
        0x0f,
        0x89, // GPS百分秒
        0x35, // GPS经度
        0x3f,
        0xf0,
        0x42,
        0x19, // GPS纬度
        0xff,
        0xf0,
        0x41,
        0x03, // 定位星数
        0x00, // GPS高度
        0x00,
        0x61,
        0x44,
        0x00, // 空速
        0x00,
        0x5a, // 相对高度
        0x00,
        0x00, // 云台控制命令字，70启用，00禁用
        0x00, // 方位速度, L:f6ff(-10), R:0a00(10)
        0x00,
        0x00, // 俯仰速度，U:0a00(10), D:f6ff(-10)
        0x00,
        0x00, // 校验字节 3-42字节异或低8位
        0xf0 // 帧尾同步码
    };

    void Start()
    {
        InvokeRepeating("CommandCamera", 0.0f, 0.04f); // 40ms, 25Hz
        cameraIP = IPAddress.Parse(cameraIPStr);
        headTracker = GetComponent<HeadTracker>();
    }

    void CommandCamera()
    {
        Int16 azimuthSpeed = AngleMapping(headTracker.AzimuthAngle);
        Int16 pitchSpeed = AngleMapping(headTracker.PitchAngle);
        Span<byte> packetDataSpan = packetData;
        // write current time
        DateTime now = DateTime.Now;
        packetData[14] = (byte)(now.Year - 2000);
        packetData[15] = (byte)now.Month;
        packetData[16] = (byte)now.Day;
        packetData[17] = (byte)now.Hour;
        packetData[18] = (byte)now.Minute;
        packetData[19] = (byte)now.Second;
        packetData[20] = (byte)(now.Millisecond / 10);

        BinaryPrimitives.WriteInt16LittleEndian(packetDataSpan.Slice(38, 2), azimuthSpeed);
        BinaryPrimitives.WriteInt16LittleEndian(packetDataSpan.Slice(40, 2), pitchSpeed);
        WriteCheckByte();

        string byteString = BitConverter.ToString(packetData); // for debugging, command out after use
        Debug.Log(byteString);
        SendPacket(packetData);
    }

    void OnEnable()
    {
        packetData[37] = 0x70;
    }

    void OnDisable()
    {
        packetData[37] = 0x00;
        WriteCheckByte();
        SendPacket(packetData);
    }

    void WriteCheckByte()
    {
        byte checkByte = 0;
        for (int i = 2; i < 42; i++)
        {
            checkByte ^= packetData[i];
        }
        packetData[42] = checkByte;
    }

    Int16 AngleMapping(float angle)
    {
        if (angle > thresholdAngle)
        {
            return (Int16)(maxSpeed);
        }
        else if (angle < -thresholdAngle)
        {
            return (Int16)(-maxSpeed);
        }
        else
        {
            return 0;
        }
    }

    // send packet to camera
    void SendPacket(byte[] packet)
    {
        UdpClient client = new UdpClient();
        client.Send(packet, packet.Length, new IPEndPoint(cameraIP, cameraPort));
        client.Close();
    }
}
