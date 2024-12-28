/**
 * @file YumiGripperStatePublisher.cs
 * @author zoequ
 * @brief Publish Gripper control topics.
 * @version 1.0
 * @date 2024
 * 
 * @copyright Flair 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Std;
using RosMessageTypes.Geometry;
using RosMessageTypes.BuiltinInterfaces;

public class YumiGripperStatePublisher : MonoBehaviour
{
    [SerializeField] private string topicName_left = "/unity/yumi_gripper_left";
    [SerializeField] private string topicName_right = "/unity/yumi_gripper_right";

    private ROSConnection ros;
    private PoseStampedMsg RightSourceGripperStateMsg = new PoseStampedMsg();
    private PoseStampedMsg LeftSourceGripperStateMsg = new PoseStampedMsg();
    private uint seq_left = 0;
    private uint seq_right = 0;


    // Start is called before the first frame update
    void Start()
    {
        // init ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        // register publisher
        ros.RegisterPublisher<PoseStampedMsg>(topicName_left);
        ros.RegisterPublisher<PoseStampedMsg>(topicName_right);

    }

    // Update is called once per frame
    void Update()
    {
        
        //if(Input.GetKeyDown(KeyCode.UpArrow))
        if (Input.GetKeyDown(KeyCode.O))
            {
            Debug.Log("===== Open Left gripper command received!!! =====");

            StartCoroutine(SendGripperState(LeftSourceGripperStateMsg, new Vector3(1.0f, 0.0f, 0.0f)));
        }

        //if (Input.GetKeyDown(KeyCode.DownArrow))
        if (Input.GetKeyDown(KeyCode.P))
            {
            Debug.Log("===== Close Left gripper command received!!! =====");

            StartCoroutine(SendGripperState(LeftSourceGripperStateMsg, new Vector3(0.0f, 1.0f, 0.0f)));
        }

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("===== Open Right gripper command received!!! =====");

            StartCoroutine(SendGripperState(RightSourceGripperStateMsg, new Vector3(1.0f, 0.0f, 0.0f)));
        }

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("===== Close Right gripper command received!!! =====");

            StartCoroutine(SendGripperState(RightSourceGripperStateMsg, new Vector3(0.0f, 1.0f, 0.0f)));
        }

        ros.Publish(topicName_left, LeftSourceGripperStateMsg);
        ros.Publish(topicName_right, RightSourceGripperStateMsg);

    }

    private IEnumerator SendGripperState(PoseStampedMsg gripperStateMsg, Vector3 position)
    {
        gripperStateMsg.header = new HeaderMsg(seq_left++, new TimeMsg(), "gripper");

        gripperStateMsg.pose.position.x = position.x;
        gripperStateMsg.pose.position.y = position.y;
        gripperStateMsg.pose.position.z = position.z;

        ros.Publish(topicName_left, LeftSourceGripperStateMsg);
        ros.Publish(topicName_right, RightSourceGripperStateMsg);

        yield return new WaitForSeconds(1.0f);

        gripperStateMsg.pose.position.x = 0.0f;
        gripperStateMsg.pose.position.y = 0.0f;
        gripperStateMsg.pose.position.z = 0.0f;

        ros.Publish(topicName_left, LeftSourceGripperStateMsg);
        ros.Publish(topicName_right, RightSourceGripperStateMsg);
    }


    public void OpenGripperLeft()
    {

        Debug.Log("===== Open left gripper command received!!! =====");

        LeftSourceGripperStateMsg.header = new HeaderMsg(seq_left++, new TimeMsg(), "gripper_left");
        LeftSourceGripperStateMsg.pose.position.x = 1.0f;
        LeftSourceGripperStateMsg.pose.position.y = 0.0f;
        LeftSourceGripperStateMsg.pose.position.z = 0.0f;

        StartCoroutine(ResetGripperState(LeftSourceGripperStateMsg));
    }
    
    public void CloseGripperLeft()
    {

        Debug.Log("===== Close left gripper command received!!! =====");

        LeftSourceGripperStateMsg.header = new HeaderMsg(seq_left++, new TimeMsg(), "gripper_left");
        LeftSourceGripperStateMsg.pose.position.x = 0.0f;
        LeftSourceGripperStateMsg.pose.position.y = 1.0f;
        LeftSourceGripperStateMsg.pose.position.z = 0.0f;

        StartCoroutine(ResetGripperState(LeftSourceGripperStateMsg));
    }

    public void OpenGripperRight()
    {

        Debug.Log("===== Open right gripper command received!!! =====");

        RightSourceGripperStateMsg.header = new HeaderMsg(seq_right++, new TimeMsg(), "gripper_right");
        RightSourceGripperStateMsg.pose.position.x = 1.0f;
        RightSourceGripperStateMsg.pose.position.y = 0.0f;
        RightSourceGripperStateMsg.pose.position.z = 0.0f;

        StartCoroutine(ResetGripperState(RightSourceGripperStateMsg));
    }

    public void CloseGripperRight()
    {

        Debug.Log("===== Close right gripper command received!!! =====");

        RightSourceGripperStateMsg.header = new HeaderMsg(seq_right++, new TimeMsg(), "gripper_left");
        RightSourceGripperStateMsg.pose.position.x = 0.0f;
        RightSourceGripperStateMsg.pose.position.y = 1.0f;
        RightSourceGripperStateMsg.pose.position.z = 0.0f;

        StartCoroutine(ResetGripperState(RightSourceGripperStateMsg));
    }

    private IEnumerator ResetGripperState(PoseStampedMsg gripperStateMsg)
    {
        yield return new WaitForSeconds(0.1f);

        gripperStateMsg.pose.position.x = 0.0f;
        gripperStateMsg.pose.position.y = 0.0f;
        gripperStateMsg.pose.position.z = 0.0f;

        ros.Publish(topicName_left, LeftSourceGripperStateMsg);
        ros.Publish(topicName_right, RightSourceGripperStateMsg);
    }
}
