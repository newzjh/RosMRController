using System.Collections;
using System.Collections.Generic;
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

public class ur5ARJoyBinMove : MonoBehaviour
{
    public float speed = 0.01f;
    public float rotation_speed = 1.0f;
    private bool isJoint = false;
    private bool isEuclid = true;

    private bool translation = false;
    private bool rotation = false;

    // private bool con_slider = true;
    private Vector3 moveDirection = Vector3.zero;
    // set jointDirection as a 6 dimension float array;
    private float[] jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

    public Scrollbar Mode_Slider;         //Mode min=0 max=1

    [SerializeField] private GameObject target;

    private GameObject[] links;
    private GameObject baseLink;
    private string routeToLink = "world/base_link";
    private bool TrailVisualable = true;

    private Vector3 start_pos;
    private Vector3 start_rot;


    // ------------------------------------------------------------------------------
    public void moveEuc1Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[6] { -1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    }
    public void moveEuc1Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[6] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    }

    public void moveEuc2Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    }
    public void moveEuc2Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    }

    public void moveEuc3Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f };
    }
    public void moveEuc3Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f };
    }

    public void moveEuc4Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f };
    }
    public void moveEuc4Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f };
    }

    public void moveEuc5Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, -1.0f, 0.0f };
    }
    public void moveEuc5Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f };
    }

    public void moveEuc6Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, -1.0f };
    }
    public void moveEuc6Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f };
    }
    // ------------------------------------------------------------------------------

    public void stop()
    {
        moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    }

    public void ur5ControlTrailRenderer()
    {
        if (TrailVisualable)
        {
            TrailRenderer trailRenderer = GameObject.Find("ur5_gripper_controller").GetComponent<TrailRenderer>();
            trailRenderer.enabled = true;
            TrailVisualable = false;

        }
        else
        {
            TrailRenderer trailRenderer = GameObject.Find("ur5_gripper_controller").GetComponent<TrailRenderer>();
            trailRenderer.enabled = false;
            TrailVisualable = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        baseLink = GameObject.Find(routeToLink).gameObject;
        links = new GameObject[6];
        routeToLink += "/shoulder_link";
        links[0] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/upper_arm_link";
        links[1] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/forearm_link";
        links[2] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/wrist_1_link";
        links[3] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/wrist_2_link";
        links[4] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/wrist_3_link";
        links[5] = GameObject.Find(routeToLink).gameObject;

        Mode_Slider.value = 0f;
    }

    // Update is called once per frame
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
        }

        if (isJoint)// Mode_Slider.value > 0.5f
        {

            // Debug.Log("isJoint");
            for (int i = 0; i < 6; i++)
            {
                if (jointDirection[i] == 0.0f)
                    continue;
                if (i == 0)
                    links[i].transform.localEulerAngles += new Vector3(0.0f, jointDirection[i], 0.0f) * speed * 100 * Time.deltaTime;
                else
                    links[i].transform.localEulerAngles += new Vector3(jointDirection[i], 0.0f, 0.0f) * speed * 100 * Time.deltaTime;
            }
        }
    }
}
