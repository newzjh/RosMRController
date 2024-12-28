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

public class YumiMotion_ARControl : MonoBehaviour
{
    public float speed = 0.01f;
    public float rotation_speed = 1;
    private bool isJoint = false;
    private bool isEuclid = true;

    private bool translation = false;
    private bool rotation = false;

    // private bool con_slider = true;
    private Vector3 moveDirection = Vector3.zero;
    // set jointDirection as a 7 dimension float array;
    private float[] jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };


    public Scrollbar Mode_Slider;         //Mode min=0 max=1

    // ------------------------------------------------------------------------------
    private bool TrailVisualable = true;
    private bool RightArmVisible = true;
    private GameObject target;
    private GameObject target2;
    private GameObject RightBaseLink;
    private GameObject LeftBaseLink;
    private GameObject[] Leftlinks;
    private string LeftRouteToLink = "world/yumi_base_link/yumi_body";
    // ------------------------------------------------------------------------------

    private GameObject[] links;
    private GameObject baseLink;
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
    public void moveEuc1Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[7] { -1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        target.transform.Find("translation").Find("right").gameObject.SetActive(true);
    }
    public void moveEuc1Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[7] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        target.transform.Find("translation").Find("left").gameObject.SetActive(true);
    }
    public void moveEuc2Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        target.transform.Find("translation").Find("down").gameObject.SetActive(true);
    }
    public void moveEuc2Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        target.transform.Find("translation").Find("up").gameObject.SetActive(true);
    }
    public void moveEuc3Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        target.transform.Find("translation").Find("back").gameObject.SetActive(true);
    }
    public void moveEuc3Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        target.transform.Find("translation").Find("forward").gameObject.SetActive(true);
    }
    public void moveEuc4Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f };

        target.transform.Find("rotation").Find("x-").gameObject.SetActive(true);
    }
    public void moveEuc4Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f };

        target.transform.Find("rotation").Find("x+").gameObject.SetActive(true);
    }
    public void moveEuc5Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f };

        target.transform.Find("rotation").Find("y-").gameObject.SetActive(true);
    }
    public void moveEuc5Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f };

        target.transform.Find("rotation").Find("y+").gameObject.SetActive(true);
    }
    public void moveEuc6Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, -1.0f, 0.0f };

        target.transform.Find("rotation").Find("z-").gameObject.SetActive(true);
    }
    public void moveEuc6Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f };

        target.transform.Find("rotation").Find("z+").gameObject.SetActive(true);
    }
    public void moveJ7Minus()
    {
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, -1.0f };
    }
    public void moveJ7Plus()
    {
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f };
    }
    public void stop()
    {
        moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        Transform trotation = target.transform.Find("rotation");
        for (int i = 0; i < trotation.childCount; i++)
            trotation.GetChild(i).gameObject.SetActive(false);

        Transform ttranslation = target.transform.Find("translation");
        for (int i = 0; i < ttranslation.childCount; i++)
            ttranslation.GetChild(i).gameObject.SetActive(false);
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

    void Update()
    {
        if (Mode_Slider.value < 0.5f)
        {
            isEuclid = true;
            isJoint = false;
        }
        else
        {
            isEuclid = false;
            isJoint = true;
        }


        if (isEuclid) //EE Moving
        {
            if (translation)
            {
                target.transform.localPosition += moveDirection * speed * Time.deltaTime;
            }
            else if (rotation)
            {
                target.transform.eulerAngles += moveDirection * rotation_speed * 10 * Time.deltaTime;
            }
        }
    }
}
