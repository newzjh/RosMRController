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
using Unity.Robotics.ROSTCPConnector.ROSGeometry;

public class JointPublisher : MonoBehaviour
{
    [SerializeField] private string topicName = "HoloLens_d";

    [SerializeField] private GameObject eemanipulator;

    private uint seq = 0;
    private ROSConnection ros;
    //public Vector3 currentSent;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance(); //init ros connection
        // ros.RegisterPublisher<PointStampedMsg>(topicName);
        ros.RegisterPublisher<PoseStampedMsg>(topicName);

    }

    void Update()
    {
        /********************************************************/
        // geometry_msgs/PointStamped Message position x, y, z
        // Vector3 eePositionValue = eemanipulator.GetComponent<EndEffectorController>().eePositionValue;
        // PointStampedMsg sourceEePositionStatemsg = new PointStampedMsg();
        // sourceEePositionStatemsg.header = new HeaderMsg(seq++, new TimeMsg(), "base");
        // sourceEePositionStatemsg.point.y = -eePositionValue.x;
        // sourceEePositionStatemsg.point.z = eePositionValue.y;
        // sourceEePositionStatemsg.point.x = eePositionValue.z;

        // ros.Publish(topicName, sourceEePositionStatemsg);

        /*********************************************************/

        //currentSent[1] = jointManipValue.x;
        //currentSent[2] = jointManipValue.y;
        //currentSent[0] = -jointManipValue.z;

        // geometry_msgs/Pose Message position + orientation
        // orientatin is represeented in quaternion x, y, z, w; transform.local rotation
        Vector3 eePositionValue = eemanipulator.GetComponent<EndEffectorController>().eePositionValue;
  
        Quaternion eeRotationValue = eemanipulator.GetComponent<EndEffectorController>().eeRotationValue;

        PoseStampedMsg sourceEeManipPoseStatemsg = new PoseStampedMsg();
        sourceEeManipPoseStatemsg.header = new HeaderMsg(seq++, new TimeMsg(), "base");
        sourceEeManipPoseStatemsg.pose.position = eePositionValue.To<FLU>();
        
        sourceEeManipPoseStatemsg.pose.orientation = eeRotationValue.To<FLU>();
        // Debug.Log("Orientation in ROS: " + sourceEeManipPoseStatemsg.pose.orientation.w);
        // Debug.Log("Orientation in ROS: " + sourceEeManipPoseStatemsg.pose.orientation.x);
        // Debug.Log("Orientation in ROS: " + sourceEeManipPoseStatemsg.pose.orientation.y);
        // Debug.Log("Orientation in ROS: " + sourceEeManipPoseStatemsg.pose.orientation.z);
        ros.Publish(topicName, sourceEeManipPoseStatemsg);

        // pose following function.
        
    }
}
