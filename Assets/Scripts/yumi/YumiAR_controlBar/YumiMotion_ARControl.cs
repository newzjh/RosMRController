/**
 * @file YumiMotion_ARControl.cs
 * @author zoequ
 * @brief All AR functions. Control dual-EE of abb-yumi by 3D panel in AR environment.
 * @version 1.0
 * @date 2024
 * 
 * @copyright Flair 2024
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RosMessageTypes.Std;
using RosMessageTypes.Geometry;
using RosMessageTypes.BuiltinInterfaces;
using MixedReality.Toolkit;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;
using TMPro;
using UnityEngine.UIElements;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
//using static UnityEditor.PlayerSettings;
using UnityEngine.XR.ARFoundation;

public class YumiMotion_ARControl : Motion_ARControlBase
{
 



    // ------------------------------------------------------------------------------
    private bool TrailVisualable = true;
    private bool RightArmVisible = true;
    private GameObject target2;
    private GameObject RightBaseLink;
    private GameObject LeftBaseLink;
    private GameObject[] Leftlinks;
    private string LeftRouteToLink = "world/yumi_base_link/yumi_body";
    // ------------------------------------------------------------------------------

    private string routeToLink = "world/yumi_base_link/yumi_body";

    private Vector3 start_pos;
    private Vector3 start_rot;
    // ------------------------------------------------------------------------------
    [SerializeField] private string reset_msg = "/unity/go_home";

    private uint seq = 0;
    private GameObject yumibody;
    [SerializeField] private GameObject Body;
    [SerializeField] private GameObject RightEE;
    [SerializeField] private GameObject LeftEE;
    public float EEMoveSpeed = 5f;
    private ROSConnection ros;
    // ------------------------------------------------------------------------------

    public void goHome()
    {
        yumibody = GameObject.Find("world/yumi_base_link/yumi_body").gameObject;
        Vector3 relativePositionL = new Vector3(-0.26f, 0.28f, -0.06f);
        Quaternion relativeRotationL = Quaternion.Euler(-68.73f, 357.68f, -180.03f);
        Vector3 relativePositionR = new Vector3(0.26f, 0.28f, -0.06f);
        Quaternion relativeRotationR = Quaternion.Euler(-69.63f, 3.33f, -180.56f);

        LeftEE.transform.position = yumibody.transform.TransformPoint(relativePositionL);
        LeftEE.transform.rotation = yumibody.transform.rotation * relativeRotationL;

        RightEE.transform.position = yumibody.transform.TransformPoint(relativePositionR);
        RightEE.transform.rotation = yumibody.transform.rotation * relativeRotationR;

        PoseStampedMsg ResetStatemsg = new PoseStampedMsg();
        ResetStatemsg.header = new HeaderMsg(seq++, new TimeMsg(), "base");
        ResetStatemsg.pose.position.x = 1.0f;
        ResetStatemsg.pose.position.y = 1.0f;
        ResetStatemsg.pose.position.z = 1.0f;

        ros.Publish(reset_msg, ResetStatemsg);
    }
  
   
    public void armswitch()
    {
        if (RightArmVisible)
        {
            target = GameObject.Find("world/yumi_base_link/gripper_l_controller").gameObject;
            GameObject.Find("Arm_switch/ArmInfo").GetComponent<TMP_Text>().text = "Left Arm";
            RightArmVisible = false;
        }

        else
        {
            target = GameObject.Find("world/yumi_base_link/gripper_r_controller").gameObject;
            GameObject.Find("Arm_switch/ArmInfo").GetComponent<TMP_Text>().text = "Right Arm";
            RightArmVisible = true;
        }
    }
    public void controlTrailRenderer()
    {
        if (TrailVisualable)
        {
            TrailRenderer trailRenderer1 = GameObject.Find("gripper_r_controller").GetComponent<TrailRenderer>();
            trailRenderer1.enabled = true;
            TrailRenderer trailRenderer2 = GameObject.Find("gripper_l_controller").GetComponent<TrailRenderer>();
            trailRenderer2.enabled = true;
            TrailVisualable = false;

        }
        else {
            TrailRenderer trailRenderer1 = GameObject.Find("gripper_r_controller").GetComponent<TrailRenderer>();
            trailRenderer1.enabled = false;
            TrailRenderer trailRenderer2 = GameObject.Find("gripper_l_controller").GetComponent<TrailRenderer>();
            trailRenderer2.enabled = false;
            TrailVisualable = true;
        }
    }



    void Start()
    {
        RightBaseLink = GameObject.Find(routeToLink).gameObject;
        links = new GameObject[7];
        routeToLink += "/yumi_link_1_r";
        links[0] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_2_r";
        links[1] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_3_r";
        links[2] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_4_r";
        links[3] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_5_r";
        links[4] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_6_r";
        links[5] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/yumi_link_7_r";
        links[6] = GameObject.Find(routeToLink).gameObject;

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

        target = GameObject.Find("world/yumi_base_link/gripper_r_controller").gameObject; // default target


        ////// initinal controllers' position.
        yumibody = GameObject.Find("world/yumi_base_link/yumi_body").gameObject;
        Vector3 relativePositionL = new Vector3(-0.26f, 0.28f, -0.06f);
        Quaternion relativeRotationL = Quaternion.Euler(-68.73f, 357.68f, -180.03f);
        Vector3 relativePositionR = new Vector3(0.26f, 0.28f, -0.06f);
        Quaternion relativeRotationR = Quaternion.Euler(-69.63f, 3.33f, -180.56f);

        LeftEE.transform.position = yumibody.transform.TransformPoint(relativePositionL);
        LeftEE.transform.rotation = yumibody.transform.rotation * relativeRotationL;

        RightEE.transform.position = yumibody.transform.TransformPoint(relativePositionR);
        RightEE.transform.rotation = yumibody.transform.rotation * relativeRotationR;

        ros = ROSConnection.GetOrCreateInstance(); //init ros connection
        ros.RegisterPublisher<PoseStampedMsg>(reset_msg);
    }

}
