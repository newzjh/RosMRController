/**
 * @file YumiBinMove.cs 
 * @author zoequ
 * @brief Move the dual-arm of Yumi, follow Yuxin Chen work
 * @version 0.1
 * @date 2023
 * 
 * @copyright Copyright Flair 2023
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RosMessageTypes.Std;
using RosMessageTypes.Geometry;
using RosMessageTypes.BuiltinInterfaces;
using System.Linq;
// using Unity.Robotics.ROSTCPConnector;
// using Unity.Robotics.ROSTCPConnector.ROSGeometry;

public class YumiArmJoyBinMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float speed = 1.0f;
    public float rotation_speed = 5.0f;
    private bool isJoint = false;
    private bool isEuclid = true;

    private bool translation = false;
    private bool rotation = false;

    // private bool con_slider = true;
    private Vector3 moveDirection = Vector3.zero;
    // set jointDirection as a 7 dimension float array;
    private float[] jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    

    public Scrollbar Mode_Slider;         //Mode min=0 max=1

    // public GameObject target;

    // ------------------------------------------------------------------------------
    public Scrollbar Arm_Slider;          //New
    private GameObject target;
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

    // [SerializeField] private string topicName = "point";
    // private ROSConnection ros;
    // private uint seq = 0;

    // ------------------------------------------------------------------------------
    public void moveEuc1Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[7]{-1.0f, 0.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }
    public void moveEuc1Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[7]{1.0f, 0.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }

    public void moveEuc2Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[7]{0.0f, -1.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }
    public void moveEuc2Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 1.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }

    public void moveEuc3Minus()
    {
        //GameObject attachedGameObject = gameObject.transform.parent.gameObject;
        //Debug.Log("Joint3 Button is attached to: " + attachedGameObject.name);

        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[7]{0.0f, 0.0f, -1.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }
    public void moveEuc3Plus()
    {
        //GameObject attachedGameObject = gameObject.transform.parent.gameObject;
        //Debug.Log("Joint3 Button is attached to: " + attachedGameObject.name);

        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 1.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }

    public void moveEuc4Minus()
    {
        //GameObject attachedGameObject = gameObject.transform.parent.gameObject;
        //Debug.Log("Joint4 Button is attached to: " + attachedGameObject.name);

        translation = false;
        rotation = true;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, -1.0f, 0.0f ,0.0f, 0.0f};

    }
    public void moveEuc4Plus()
    {
        //GameObject attachedGameObject = gameObject.transform.parent.gameObject;
        //Debug.Log("Joint4 Button is attached to: " + attachedGameObject.name);

        translation = false;
        rotation = true;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 1.0f, 0.0f ,0.0f, 0.0f};
    }

    public void moveEuc5Minus()
    {
        //GameObject attachedGameObject = gameObject.transform.parent.gameObject;
        //Debug.Log("Joint5 Button is attached to: " + attachedGameObject.name);

        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, -1.0f ,0.0f, 0.0f};
    }
    public void moveEuc5Plus()
    {
        //GameObject attachedGameObject = gameObject.transform.parent.gameObject;
        //Debug.Log("Joint5 Button is attached to: " + attachedGameObject.name);

        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 1.0f ,0.0f, 0.0f};
    }

    public void moveEuc6Minus()
    {
        //GameObject attachedGameObject = gameObject.transform.parent.gameObject;
        //Debug.Log("Joint6 Button is attached to: " + attachedGameObject.name);

        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,-1.0f, 0.0f};
    }
    public void moveEuc6Plus()
    {
        //GameObject attachedGameObject = gameObject.transform.parent.gameObject;
        //Debug.Log("Joint6 Button is attached to: " + attachedGameObject.name);

        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,1.0f, 0.0f};
    }
    // ------------------------------------------------------------------------------
    public void moveJ7Minus()
    {
        //GameObject attachedGameObject = gameObject.transform.parent.gameObject;
        //Debug.Log("Joint7 Button is attached to: " + attachedGameObject.name);

        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, -1.0f };
    }
    public void moveJ7Plus()
    {
        //GameObject attachedGameObject = gameObject.transform.parent.gameObject;
        //Debug.Log("Joint7 Button is attached to: " + attachedGameObject.name);

        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f };
    }

    // ------------------------------------------------------------------------------
    public void stop()
    {
        moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //stop();
        //Debug.Log("OnPointerUp");
    }

    
    void Start()
    {
        // start_pos = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, target.transform.localPosition.z);
        // start_rot = new Vector3(target.transform.eulerAngles.x, target.transform.eulerAngles.y, target.transform.eulerAngles.z);
        // ros = ROSConnection.GetOrCreateInstance();
        // ros.RegisterPublisher<PointStampedMsg>(topicName);
        // baseLink = GameObject.Find(routeToLink).gameObject;

        RightBaseLink = GameObject.Find(routeToLink).gameObject;
        links = new GameObject[7];
        //Debug.Log("Links: " + links);
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

        // Debug.Log("Rightlinks: " + string.Join(", ", links.Select(link => link.ToString()).ToArray()));

        // ------------------------------------------------------------------------------
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

        // Debug.Log("Leftlinks: " + string.Join(", ", Leftlinks.Select(link => link.ToString()).ToArray()));
        // ------------------------------------------------------------------------------
    }

    void Update()
    {

        if (Arm_Slider.value < 0.5f)
        {
            target = GameObject.Find("world/yumi_base_link/gripper_r_controller").gameObject;

        }

        else
        {
            target = GameObject.Find("world/yumi_base_link/gripper_l_controller").gameObject;
        }

        // Debug.Log("target is :" + target.ToString());
        // Debug.Log("update with direction" + moveDirection);
        // Debug.Log("uMode_Slider.value" + Mode_Slider.value);

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

        // Debug.Log("isJoy: " + isJoy);
        // Debug.Log("isBar: " + isBar);
        // Debug.Log("start_pos: " + start_pos);
        // Debug.Log("start_rot: " + start_rot);

        if (isEuclid) //isMoving
        {
            // Debug.Log("isEuclid");
            if (translation)
            {
                target.transform.localPosition += moveDirection * speed * Time.deltaTime;
            }
            else if (rotation)
            {
                target.transform.eulerAngles += moveDirection * rotation_speed * 10 * Time.deltaTime;
            }

            // Debug.Log("target.transform.eulerAngles.x: " + target.transform.eulerAngles.x);

            // TX_Slider.value = target.transform.localPosition.x - start_pos.x;
            // TY_Slider.value = target.transform.localPosition.y - start_pos.y;
            // TZ_Slider.value = target.transform.localPosition.z - start_pos.z;
            // con_slider = false;
        }

        if (isJoint && (Arm_Slider.value < 0.5f)) // control right arm joint
        {
            for (int i = 0; i < 7; i++)
            {
                Debug.Log("Joint is: " + links[i]); // 
                if (jointDirection[i] == 0.0f)
                    continue;
                if (i == 0)
                    links[i].transform.localEulerAngles += new Vector3(0.0f, jointDirection[i], 0.0f) * speed * 100 * Time.deltaTime;
                else
                    links[i].transform.localEulerAngles += new Vector3(jointDirection[i], 0.0f, 0.0f) * speed * 100 * Time.deltaTime;
            }
        }

        if (isJoint && (Arm_Slider.value > 0.5f)) // control left arm joint
        {

            // Debug.Log("isJoint");
            for (int i = 0; i < 7; i++)
            {
                if (jointDirection[i] == 0.0f)
                    continue;
                if (i == 0)
                    Leftlinks[i].transform.localEulerAngles += new Vector3(0.0f, jointDirection[i], 0.0f) * speed * -100 * Time.deltaTime;
                else
                    Leftlinks[i].transform.localEulerAngles += new Vector3(jointDirection[i], 0.0f, 0.0f) * speed * 100 * Time.deltaTime;
            }
        }
    }
}