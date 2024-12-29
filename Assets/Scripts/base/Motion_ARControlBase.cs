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

public class Motion_ARControlBase : MonoBehaviour
{
    public float speed = 0.01f;
    public float rotation_speed = 1;
    protected bool isJoint = false;
    protected bool isEuclid = true;

    protected bool translation = false;
    protected bool rotation = false;

    // private bool con_slider = true;
    protected Vector3 moveDirection = Vector3.zero;
    // set jointDirection as a 7 dimension float array;
    protected float[] jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };


    public Scrollbar Mode_Slider;         //Mode min=0 max=1

    // ------------------------------------------------------------------------------
    protected GameObject target;

    // ------------------------------------------------------------------------------

    protected GameObject[] links;
    protected GameObject baseLink;

    // ------------------------------------------------------------------------------
    public static Motion_ARControlBase Instance;
    public void Awake()
    {
        Instance = this;
    }

    public void moveEuc1Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[7] { -1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        if (target)
            target.transform.Find("translation").Find("right").gameObject.SetActive(true);
    }
    public void moveEuc1Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[7] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        if (target)
            target.transform.Find("translation").Find("left").gameObject.SetActive(true);
    }
    public void moveEuc2Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        if (target)
            target.transform.Find("translation").Find("down").gameObject.SetActive(true);
    }
    public void moveEuc2Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        if (target)
            target.transform.Find("translation").Find("up").gameObject.SetActive(true);
    }
    public void moveEuc3Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        if (target)
            target.transform.Find("translation").Find("back").gameObject.SetActive(true);
    }
    public void moveEuc3Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        if (target)
            target.transform.Find("translation").Find("forward").gameObject.SetActive(true);
    }
    public void moveEuc4Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f };

        if (target)
            target.transform.Find("rotation").Find("x-").gameObject.SetActive(true);
    }
    public void moveEuc4Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f };

        if (target)
            target.transform.Find("rotation").Find("x+").gameObject.SetActive(true);
    }
    public void moveEuc5Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f };

        if (target)
            target.transform.Find("rotation").Find("y-").gameObject.SetActive(true);
    }
    public void moveEuc5Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f };

        if (target)
            target.transform.Find("rotation").Find("y+").gameObject.SetActive(true);
    }
    public void moveEuc6Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, -1.0f, 0.0f };

        if (target)
            target.transform.Find("rotation").Find("z-").gameObject.SetActive(true);
    }
    public void moveEuc6Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[7] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f };

        if (target)
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



    protected void Update()
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
        if (isJoint)// Mode_Slider.value > 0.5f
        {

            for (int i = 0; i < 7; i++)
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
