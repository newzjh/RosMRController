/**
 * @file ur5JoyBinMove.cs
 * @author ZoeQU
 * @brief Control the joint movement of ur5
 * @version 0.1
 * @date 2024-07-11
 * 
 * @copyright Copyright Flair 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RosMessageTypes.Std;
using RosMessageTypes.Geometry;
using RosMessageTypes.BuiltinInterfaces;
using System.Linq;

public class ur5JoyBinMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float speed = 1.0f;
    public float rotation_speed = 5.0f;
    private bool isJoint = false;
    private bool isEuclid = true;

    private bool translation = false;
    private bool rotation = false;

    // private bool con_slider = true;
    private Vector3 moveDirection = Vector3.zero;
    // set jointDirection as a 6 dimension float array;
    private float[] jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};

    public Scrollbar Mode_Slider;         //Mode min=0 max=1

    [SerializeField] private GameObject target;

    private GameObject[] links;
    private GameObject baseLink;
    private string routeToLink = "world/base_link";

    private Vector3 start_pos;
    private Vector3 start_rot;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        // stop();
        Debug.Log("OnPointerUp");
    }

    // ------------------------------------------------------------------------------
    public void moveEuc1Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[6] { -1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    }
    public void moveEuc1Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[6] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    }

    public void moveEuc2Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    }
    public void moveEuc2Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    }

    public void moveEuc3Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f};
    }
    public void moveEuc3Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f};
    }

    public void moveEuc4Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f};
    }
    public void moveEuc4Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f};
    }

    public void moveEuc5Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, -1.0f, 0.0f};
    }
    public void moveEuc5Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f};
    }

    public void moveEuc6Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, -1.0f};
    }
    public void moveEuc6Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f};
    }
    // ------------------------------------------------------------------------------

    public void stop()
    {
        moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        jointDirection = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
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

            // float px, py, pz, rx, ry, rz;
            // px = TX_Slider.value;
            // py = TY_Slider.value;
            // pz = TZ_Slider.value;
            // rx = RX_Slider.value;
            // ry = RY_Slider.value;
            // rz = RZ_Slider.value;
            // Debug.Log("px: " + px);
            // Debug.Log("py: " + py);
            // Debug.Log("pz: " + pz);
            // Debug.Log("rx: " + rx);
            // Debug.Log("ry: " + ry);
            // Debug.Log("rz: " + rz);
            // target.transform.localPosition = new Vector3(px, py, pz) + start_pos;
            // target.transform.eulerAngles = new Vector3(rx, ry, rz) + start_rot;
            // Debug.Log("localPosition: " + target.transform.localPosition);
            // Debug.Log("eulerAngles: " + target.transform.eulerAngles);
            // PointStampedMsg sourceEePositionStatemsg = new PointStampedMsg();
            // sourceEePositionStatemsg.header = new HeaderMsg(seq++, new TimeMsg(), "base");
            // sourceEePositionStatemsg.point.y = px;
            // sourceEePositionStatemsg.point.z = py;
            // sourceEePositionStatemsg.point.x = pz;

            // ros.Publish(topicName, sourceEePositionStatemsg);
        }
    }
}
