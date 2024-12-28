/**
 * @file YumiJointController.cs
 * @author zoe
 * @brief Adjust yumi's joints in Unity.
 * @version 1.0
 * @date 2024 
 * 
 * @copyright Copyright Flair
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Std;
using RosMessageTypes.Sensor;
using RosMessageTypes.Geometry;
using System.Collections;

public class YumiJointController : MonoBehaviour
{

    [SerializeField] private string topicName_L = "/yumi/left_joints/";
    [SerializeField] private string topicName_R = "/yumi/right_joints/";
    private string RightRouteToLink = "world/yumi_base_link/yumi_body";
    private ROSConnection ros;
    private GameObject RightBaseLink;
    private GameObject[] Rightlinks;
    private GameObject LeftBaseLink;
    private GameObject[] Leftlinks;
    private string LeftRouteToLink = "world/yumi_base_link/yumi_body";
    private Quaternion fixedThetaLeft;
    private Quaternion fixedThetaRight;

    private void UpdateJointStates_L(JointStateMsg msg)
    {
            List<float> jointPose = new List<float>();
            foreach (double pos in msg.position)
            {
                jointPose.Add(Convert.ToSingle(pos));
            }

            List<float> jointAngles = new List<float>();
            foreach (float pp in jointPose)
            {
                jointAngles.Add(pp * Mathf.Rad2Deg);
            }

            List<string> jointNames = new List<string>(msg.name);

            int index_l_1 = Array.FindIndex(msg.name, item => item == "yumi_joint_1_l");
            int index_l_2 = Array.FindIndex(msg.name, item => item == "yumi_joint_2_l");
            int index_l_3 = Array.FindIndex(msg.name, item => item == "yumi_joint_3_l");
            int index_l_4 = Array.FindIndex(msg.name, item => item == "yumi_joint_4_l");
            int index_l_5 = Array.FindIndex(msg.name, item => item == "yumi_joint_5_l");
            int index_l_6 = Array.FindIndex(msg.name, item => item == "yumi_joint_6_l");
            int index_l_7 = Array.FindIndex(msg.name, item => item == "yumi_joint_7_l");

            Leftlinks[0].transform.rotation = fixedThetaLeft * LeftBaseLink.transform.rotation * Quaternion.Euler(0, -jointAngles[index_l_1], 0); //
            Leftlinks[1].transform.rotation = Leftlinks[0].transform.rotation * Quaternion.Euler(-jointAngles[index_l_2], 0, -90);
            Leftlinks[2].transform.rotation = Leftlinks[1].transform.rotation * Quaternion.Euler(jointAngles[index_l_7], 0, 90);
            Leftlinks[3].transform.rotation = Leftlinks[2].transform.rotation * Quaternion.Euler(-jointAngles[index_l_3] - 90, 0, -90);
            Leftlinks[4].transform.rotation = Leftlinks[3].transform.rotation * Quaternion.Euler(jointAngles[index_l_4], 0, 90); // 
            Leftlinks[5].transform.rotation = Leftlinks[4].transform.rotation * Quaternion.Euler(-jointAngles[index_l_5], 0, -90);
            Leftlinks[6].transform.rotation = Leftlinks[5].transform.rotation * Quaternion.Euler(-jointAngles[index_l_6], 0, 90);  //

    }

    private void UpdateJointStates_R(JointStateMsg msg)
    {
            List<float> jointPose = new List<float>();
            foreach (double pos in msg.position)
            {
                jointPose.Add(Convert.ToSingle(pos));
            }

            List<float> jointAngles = new List<float>();
            foreach (float pp in jointPose)
            {
                jointAngles.Add(pp * Mathf.Rad2Deg);
            }

            List<string> jointNames = new List<string>(msg.name);

            int index_r_1 = Array.FindIndex(msg.name, item => item == "yumi_joint_1_r");
            int index_r_2 = Array.FindIndex(msg.name, item => item == "yumi_joint_2_r");
            int index_r_3 = Array.FindIndex(msg.name, item => item == "yumi_joint_3_r");
            int index_r_4 = Array.FindIndex(msg.name, item => item == "yumi_joint_4_r");
            int index_r_5 = Array.FindIndex(msg.name, item => item == "yumi_joint_5_r");
            int index_r_6 = Array.FindIndex(msg.name, item => item == "yumi_joint_6_r");
            int index_r_7 = Array.FindIndex(msg.name, item => item == "yumi_joint_7_r");

            Rightlinks[0].transform.rotation = fixedThetaRight * RightBaseLink.transform.rotation * Quaternion.Euler(1, -jointAngles[index_r_1], 0); //
            Rightlinks[1].transform.rotation = Rightlinks[0].transform.rotation * Quaternion.Euler(-jointAngles[index_r_2], 0, -90);
            Rightlinks[2].transform.rotation = Rightlinks[1].transform.rotation * Quaternion.Euler(jointAngles[index_r_7], 0, 90);
            Rightlinks[3].transform.rotation = Rightlinks[2].transform.rotation * Quaternion.Euler(-jointAngles[index_r_3] - 90, 0, -90);
            Rightlinks[4].transform.rotation = Rightlinks[3].transform.rotation * Quaternion.Euler(jointAngles[index_r_4], 0, 90);  //
            Rightlinks[5].transform.rotation = Rightlinks[4].transform.rotation * Quaternion.Euler(-jointAngles[index_r_5], 0, -90);
            Rightlinks[6].transform.rotation = Rightlinks[5].transform.rotation * Quaternion.Euler(-jointAngles[index_r_6], 0, 90);  //
       
    }

    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();

        RightBaseLink = GameObject.Find(RightRouteToLink).gameObject;
        Rightlinks = new GameObject[7];
        RightRouteToLink += "/yumi_link_1_r";
        Rightlinks[0] = GameObject.Find(RightRouteToLink).gameObject;
        RightRouteToLink += "/yumi_link_2_r";
        Rightlinks[1] = GameObject.Find(RightRouteToLink).gameObject;
        RightRouteToLink += "/yumi_link_3_r";
        Rightlinks[2] = GameObject.Find(RightRouteToLink).gameObject;
        RightRouteToLink += "/yumi_link_4_r";
        Rightlinks[3] = GameObject.Find(RightRouteToLink).gameObject;
        RightRouteToLink += "/yumi_link_5_r";
        Rightlinks[4] = GameObject.Find(RightRouteToLink).gameObject;
        RightRouteToLink += "/yumi_link_6_r";
        Rightlinks[5] = GameObject.Find(RightRouteToLink).gameObject;
        RightRouteToLink += "/yumi_link_7_r";
        Rightlinks[6] = GameObject.Find(RightRouteToLink).gameObject;

        LeftBaseLink = GameObject.Find(LeftRouteToLink).gameObject;
        Leftlinks = new GameObject[7];
        LeftRouteToLink += "/yumi_link_1_l";
        Leftlinks[0] = GameObject.Find(LeftRouteToLink).gameObject;
        LeftRouteToLink += "/yumi_link_2_l";
        Leftlinks[1] = GameObject.Find(LeftRouteToLink).gameObject;
        LeftRouteToLink += "/yumi_link_3_l";
        Leftlinks[2] = GameObject.Find(LeftRouteToLink).gameObject;
        LeftRouteToLink += "/yumi_link_4_l";
        Leftlinks[3] = GameObject.Find(LeftRouteToLink).gameObject;
        LeftRouteToLink += "/yumi_link_5_l";
        Leftlinks[4] = GameObject.Find(LeftRouteToLink).gameObject;
        LeftRouteToLink += "/yumi_link_6_l";
        Leftlinks[5] = GameObject.Find(LeftRouteToLink).gameObject;
        LeftRouteToLink += "/yumi_link_7_l";
        Leftlinks[6] = GameObject.Find(LeftRouteToLink).gameObject;

        fixedThetaLeft = Leftlinks[0].transform.rotation * Quaternion.Inverse(LeftBaseLink.transform.rotation);
        fixedThetaRight = Rightlinks[0].transform.rotation * Quaternion.Inverse(RightBaseLink.transform.rotation);
        //Debug.Log("theta是: " + fixedThetaLeft);

        ros.Subscribe<RosMessageTypes.Sensor.JointStateMsg>(topicName_L, UpdateJointStates_L);
        ros.Subscribe<RosMessageTypes.Sensor.JointStateMsg>(topicName_R, UpdateJointStates_R);

    }

}
