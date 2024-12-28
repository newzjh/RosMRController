using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Std;
using RosMessageTypes.Geometry;
using RosMessageTypes.BuiltinInterfaces;

public class GripperStatePublisher : MonoBehaviour
{
    [SerializeField] private string topicName = "Gripper_d";

    private ROSConnection ros;
    private PoseStampedMsg sourceGripperStateMsg = new PoseStampedMsg();
    private uint seq = 0;

    [SerializeField] private GameObject _gripperOpened;
    [SerializeField] private GameObject _gripperClosed;
    [SerializeField] private GameObject _gripperInactive;

    // Start is called before the first frame update
    void Start()
    {
        // init ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        // register publisher
        ros.RegisterPublisher<PoseStampedMsg>(topicName);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Open gripper command received!!!");

            sourceGripperStateMsg.header = new HeaderMsg(seq++, new TimeMsg(), "gripper");
            sourceGripperStateMsg.pose.position.x = 1.0f;
            sourceGripperStateMsg.pose.position.y = 0.0f;
            sourceGripperStateMsg.pose.position.z = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Close gripper command received!!!");

            sourceGripperStateMsg.header = new HeaderMsg(seq++, new TimeMsg(), "gripper");
            sourceGripperStateMsg.pose.position.x = 0.0f;
            sourceGripperStateMsg.pose.position.y = 1.0f;
            sourceGripperStateMsg.pose.position.z = 0.0f;
        }

        ros.Publish(topicName, sourceGripperStateMsg);

    }

    public void OpenGripper()
    {

        Debug.Log("Open gripper command received!!!");

        sourceGripperStateMsg.header = new HeaderMsg(seq++, new TimeMsg(), "gripper");
        sourceGripperStateMsg.pose.position.x = 1.0f;
        sourceGripperStateMsg.pose.position.y = 0.0f;
        sourceGripperStateMsg.pose.position.z = 0.0f;

    }
    
    public void CloseGripper()
    {

        Debug.Log("Close gripper command received!!!");

        sourceGripperStateMsg.header = new HeaderMsg(seq++, new TimeMsg(), "gripper");
        sourceGripperStateMsg.pose.position.x = 0.0f;
        sourceGripperStateMsg.pose.position.y = 1.0f;
        sourceGripperStateMsg.pose.position.z = 0.0f;
        
    }

    public void Open2Close()
    {
        CloseGripper();
        _gripperOpened.SetActive(false);
        _gripperClosed.SetActive(true);
    }

    public void Close2Oopen()
    {
        OpenGripper();
        _gripperOpened.SetActive(true);
        _gripperClosed.SetActive(false);
    }
}
