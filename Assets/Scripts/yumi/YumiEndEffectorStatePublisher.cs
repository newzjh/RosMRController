/**
 * @file YumiEndEffectorPublisher.cs 
 * @author zoequ
 * @brief this file for publish R & L ee position to ROS side
 * @version 1.0
 * @date 2024
 * 
 * @copyright Flair
 */

using UnityEngine;
using RosMessageTypes.Geometry;
using RosMessageTypes.Std;
using RosMessageTypes.BuiltinInterfaces;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;


public class YumiEndEffectorStatePublisher : MonoBehaviour
{
    [SerializeField] private string topicName_left = "/unity/ee_pose_left"; // left arm
    [SerializeField] private string topicName_right = "/unity/ee_pose_right"; // right arm

    [SerializeField] private GameObject armBase;
    [SerializeField] private GameObject Reemanipulator;
    [SerializeField] private GameObject Leemanipulator;

    private uint seq_left = 0;
    private uint seq_right = 0;
    private ROSConnection ros;
    //public Vector3 currentSent;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance(); //init ros connection

        ros.RegisterPublisher<PoseStampedMsg>(topicName_left);
        ros.RegisterPublisher<PoseStampedMsg>(topicName_right);

    }



    void Update()
    {
      
        if (Reemanipulator != null) 
        { 
            Vector3 ReePositionValue = armBase.GetComponent<YumiEndEffectorController>().ReePositionValue;
            Quaternion ReeRotationValue = armBase.GetComponent<YumiEndEffectorController>().ReeRotationValue;

            //Debug.Log("ReePositionValue: " + ReePositionValue);
            //Debug.Log("ReeRotationValue: " + ReeRotationValue.eulerAngles);

            PoseStampedMsg sourceRightEeManipPoseStatemsg = new PoseStampedMsg();
            sourceRightEeManipPoseStatemsg.header = new HeaderMsg(seq_right++, new TimeMsg(), "base");
            sourceRightEeManipPoseStatemsg.pose.position = ReePositionValue.To<FLU>();
            sourceRightEeManipPoseStatemsg.pose.orientation = ReeRotationValue.To<FLU>();

            //Debug.Log("Right EE Pose in ROS: " + sourceRightEeManipPoseStatemsg);

            ros.Publish(topicName_right, sourceRightEeManipPoseStatemsg);
        } 

        if (Leemanipulator != null)
        { 
            Vector3 LeePositionValue = armBase.GetComponent<YumiEndEffectorController>().LeePositionValue;
            Quaternion LeeRotationValue = armBase.GetComponent<YumiEndEffectorController>().LeeRotationValue;

            //Debug.Log("LeePositionValue: " + LeePositionValue);
            //Debug.Log("LeeRotationValue: " + LeeRotationValue.eulerAngles);

            PoseStampedMsg sourceLeftEeManipPoseStatemsg = new PoseStampedMsg();
            sourceLeftEeManipPoseStatemsg.header = new HeaderMsg(seq_left++, new TimeMsg(), "base");
            sourceLeftEeManipPoseStatemsg.pose.position = LeePositionValue.To<FLU>();
            sourceLeftEeManipPoseStatemsg.pose.orientation = LeeRotationValue.To<FLU>();

            //Debug.Log("Left EE Pose in ROS: " + sourceLeftEeManipPoseStatemsg);

            //----------------------------------------- world coordinate system -------------------------------------------------
            //Vector3 UnityLeePos = armBase.GetComponent<YumiEndEffectorController>().transform.position;
            //Debug.Log("Left EE Unity Position:x,y,z " + UnityLeePos.x + " " + UnityLeePos.y + " " + UnityLeePos.z);
            //Vector3<FLU> rosLeePos = armBase.GetComponent<YumiEndEffectorController>().transform.position.To<FLU>();
            //Debug.Log("Left EE Ros Position:x,y,z " + rosLeePos.x + " " + rosLeePos.y + " " + rosLeePos.z);

            ros.Publish(topicName_left, sourceLeftEeManipPoseStatemsg);
        }

    }
}
