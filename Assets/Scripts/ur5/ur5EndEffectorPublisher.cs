/**
 * @file ur5EndEffectorPublisher.cs
 * @author ZoeQU
 * @brief Publish ee state
 * @version 0.1
 * @date 2024-07-11
 * 
 * @copyright Copyright Flair 2024
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosMessageTypes.Geometry;
using RosMessageTypes.Std;
using RosMessageTypes.BuiltinInterfaces;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;

public class ur5EndEffectorPublisher : MonoBehaviour
{
    [SerializeField] private string topicName = "HoloLens_d";

    [SerializeField] private GameObject eemanipulator;

    private uint seq = 0;
    private ROSConnection ros;

    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance(); //init ros connection
        ros.RegisterPublisher<PoseStampedMsg>(topicName);
    }

    // Update is called once per frame
    void Update()
    {
        if (eemanipulator != null)
        {
            Vector3 eePositionValue = eemanipulator.GetComponent<ur5EndEffectorController>().eePositionValue;

            Quaternion eeRotationValue = eemanipulator.GetComponent<ur5EndEffectorController>().eeRotationValue;

            PoseStampedMsg sourceEeManipPoseStatemsg = new PoseStampedMsg();
            sourceEeManipPoseStatemsg.header = new HeaderMsg(seq++, new TimeMsg(), "base");
            sourceEeManipPoseStatemsg.pose.position = eePositionValue.To<FLU>();

            sourceEeManipPoseStatemsg.pose.orientation = eeRotationValue.To<FLU>();
            // Debug.Log("Orientation in ROS: " + sourceEeManipPoseStatemsg.pose.orientation.w);
            // Debug.Log("Orientation in ROS: " + sourceEeManipPoseStatemsg.pose.orientation.x);
            // Debug.Log("Orientation in ROS: " + sourceEeManipPoseStatemsg.pose.orientation.y);
            // Debug.Log("Orientation in ROS: " + sourceEeManipPoseStatemsg.pose.orientation.z);
            ros.Publish(topicName, sourceEeManipPoseStatemsg);
        }
    }
}
