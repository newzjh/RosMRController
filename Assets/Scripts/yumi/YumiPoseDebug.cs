/**
 * @file YumiPoseDebug.cs 
 * @author zoequ
 * @brief this file for yumi joints control test and debug (not necessary).
 * @version 1.0
 * @date 2024
 * 
 * @copyright Flair
 */

using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;
using RosMessageTypes.Geometry;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
//using static UnityEditor.PlayerSettings;

public class YumiPoseDebug : MonoBehaviour
{
    [SerializeField] private string topicName = "/yumi/left_joint_states";

    [SerializeField] private float[] jointPosDisplay = new float[7];

    private string routeToLink = "world/yumi_base_link/yumi_body";
    private ROSConnection ros;

    private GameObject baseLink;
    private GameObject RightBaseLink;
    private GameObject[] Rightlinks;
    private GameObject LeftEE;
    private GameObject RightEE;
    private GameObject yumibody;

    private GameObject LeftBaseLink;
    private GameObject[] Leftlinks;
    private string LeftRouteToLink = "world/yumi_base_link/yumi_body";

    private Quaternion fixedTheta;
    private Quaternion fixedThetaRight;

    // Start is called before the first frame update
    void Start()
    {
        RightBaseLink = GameObject.Find(routeToLink).gameObject;
        Rightlinks = new GameObject[7];
        routeToLink += "/yumi_link_1_r";
        Rightlinks[0] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_2_r";
        Rightlinks[1] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_3_r";
        Rightlinks[2] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_4_r";
        Rightlinks[3] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_5_r";
        Rightlinks[4] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_6_r";
        Rightlinks[5] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_7_r";
        Rightlinks[6] = GameObject.Find(routeToLink).gameObject;

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

        LeftEE = GameObject.Find("world/yumi_base_link/gripper_l_controller").gameObject;
        RightEE = GameObject.Find("world/yumi_base_link/gripper_r_controller").gameObject;

        fixedTheta = Leftlinks[0].transform.rotation * Quaternion.Inverse(LeftBaseLink.transform.rotation);
        fixedThetaRight = Rightlinks[0].transform.rotation * Quaternion.Inverse(RightBaseLink.transform.rotation);
        
        //Debug.Log("theta是: " + fixedTheta);

    }

    // Update is called once per frame
    void Update()
    {
        float[] jointAngles = new float[7];
        // ------------------ for debug only ---------------------
        jointAngles[0] = 12.92f;
        jointAngles[1] = -115.43f;
        jointAngles[2] = 29.76f;
        jointAngles[3] = -19.94f;
        jointAngles[4] = 60.82f;
        jointAngles[5] = -7.55f;
        jointAngles[6] = 131.9f;

        float[] jointAnglesR = new float[7];
        // ------------------ for debug only ---------------------
        jointAnglesR[0] = -12.92f;
        jointAnglesR[1] = -115.43f;
        jointAnglesR[2] = 29.76f;
        jointAnglesR[3] = 19.94f;
        jointAnglesR[4] = 60.82f;
        jointAnglesR[5] = -7.55f;
        jointAnglesR[6] = -131.9f;


        //// arms
        Leftlinks[0].transform.rotation = fixedTheta * LeftBaseLink.transform.rotation * Quaternion.Euler(0, -jointAngles[0], 0);
        Leftlinks[1].transform.rotation = Leftlinks[0].transform.rotation * Quaternion.Euler(-jointAngles[1], 0, -90);
        Leftlinks[2].transform.rotation = Leftlinks[1].transform.rotation * Quaternion.Euler(jointAngles[6], 0, 90);
        Leftlinks[3].transform.rotation = Leftlinks[2].transform.rotation * Quaternion.Euler(-jointAngles[2] - 90, 0, -90);
        Leftlinks[4].transform.rotation = Leftlinks[3].transform.rotation * Quaternion.Euler(jointAngles[3], 0, 90);
        Leftlinks[5].transform.rotation = Leftlinks[4].transform.rotation * Quaternion.Euler(-jointAngles[4], 0, -90);
        Leftlinks[6].transform.rotation = Leftlinks[5].transform.rotation * Quaternion.Euler(jointAngles[5], 0, 90);

        Rightlinks[0].transform.rotation = fixedThetaRight * RightBaseLink.transform.rotation * Quaternion.Euler(1, -jointAnglesR[0], 0); //
        Rightlinks[1].transform.rotation = Rightlinks[0].transform.rotation * Quaternion.Euler(-jointAnglesR[1], 0, -90);
        Rightlinks[2].transform.rotation = Rightlinks[1].transform.rotation * Quaternion.Euler(jointAnglesR[6], 0, 90);
        Rightlinks[3].transform.rotation = Rightlinks[2].transform.rotation * Quaternion.Euler(-jointAnglesR[2] - 90, 0, -90);
        Rightlinks[4].transform.rotation = Rightlinks[3].transform.rotation * Quaternion.Euler(jointAnglesR[3], 0, 90);  //
        Rightlinks[5].transform.rotation = Rightlinks[4].transform.rotation * Quaternion.Euler(-jointAnglesR[4], 0, -90);
        Rightlinks[6].transform.rotation = Rightlinks[5].transform.rotation * Quaternion.Euler(-jointAnglesR[5], 0, 90);  //

        yumibody = GameObject.Find("world/yumi_base_link/yumi_body").gameObject;
        Debug.Log("Left: " + yumibody.transform.TransformPoint(Leftlinks[6].transform.position));
        Debug.Log("Left ROS: " + yumibody.transform.TransformPoint(Leftlinks[6].transform.position).To<FLU>());
        Debug.Log("Left: " + yumibody.transform.TransformDirection(Leftlinks[6].transform.rotation.eulerAngles));
        Debug.Log("Left ROS: " + yumibody.transform.TransformDirection(Leftlinks[6].transform.rotation.eulerAngles).To<FLU>());

        Debug.Log("Left controller: " + yumibody.transform.TransformPoint(LeftEE.transform.position));
        Debug.Log("Left controller: " + yumibody.transform.TransformDirection(LeftEE.transform.rotation.eulerAngles));


        Debug.Log("Right: " + yumibody.transform.TransformPoint(Rightlinks[6].transform.position));
        Debug.Log("Right ROS: " + yumibody.transform.TransformPoint(Rightlinks[6].transform.position).To<FLU>());
        Debug.Log("Right: " + yumibody.transform.TransformDirection(Rightlinks[6].transform.rotation.eulerAngles));
        Debug.Log("Right ROS: " + yumibody.transform.TransformDirection(Rightlinks[6].transform.rotation.eulerAngles).To<FLU>());

        Debug.Log("Right controller: " + yumibody.transform.TransformPoint(RightEE.transform.position));
        Debug.Log("Right controller: " + yumibody.transform.TransformDirection(RightEE.transform.rotation.eulerAngles));

        //// controllers
        Vector3 relativePosition = new Vector3(-0.26f, 0.28f, -0.06f);
        Quaternion relativeRotation = Quaternion.Euler( -68.73f, 357.68f, -180.03f);

        LeftEE.transform.position = yumibody.transform.TransformPoint(relativePosition);
        LeftEE.transform.rotation = yumibody.transform.rotation * relativeRotation;
    }
}
