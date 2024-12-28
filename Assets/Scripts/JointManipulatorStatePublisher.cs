/**
 * @file JointManipulatorStatePublisher.cs
 * @author Chen Chen (maker_cc@foxmail.com)
 * @brief Publish desired velocity
 * @version 0.1
 * @date 2022-09-24
 * 
 * @copyright Copyright IRM-Lab 2022
 */

using UnityEngine;
using RosMessageTypes.Geometry;
using RosMessageTypes.Std;
using RosMessageTypes.BuiltinInterfaces;
using Unity.Robotics.ROSTCPConnector;

public class JointManipulatorStatePublisher : MonoBehaviour
{
    [SerializeField] private GameObject jointManipulator;
    [SerializeField] private string topicName = "UnityJointManipPublish";
    private uint seq = 0;
    private ROSConnection ros;
    //public Vector3 currentSent;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance(); //init ros connection
        ros.RegisterPublisher<PointStampedMsg>(topicName);
    }

    void Update()
    {
        Vector3 jointManipValue = jointManipulator.GetComponent<JointManipulatorController>().ManipValue;
        // Debug.Log("joint ManipValue: " + jointManipValue);
        PointStampedMsg sourceJointManipStateMsg = new PointStampedMsg();
        sourceJointManipStateMsg.header = new HeaderMsg(seq++, new TimeMsg(), "base");
        sourceJointManipStateMsg.point.y = jointManipValue.x; //coordinate transform from unity to ROS
        sourceJointManipStateMsg.point.z = jointManipValue.y;
        sourceJointManipStateMsg.point.x = jointManipValue.z;

        //currentSent[1] = jointManipValue.x;
        //currentSent[2] = jointManipValue.y;
        //currentSent[0] = -jointManipValue.z;

        ros.Publish(topicName, sourceJointManipStateMsg);
    }
}
