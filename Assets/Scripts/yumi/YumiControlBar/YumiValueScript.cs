/**
 * @file YumiValueScript.cs 
 * @author zoequ
 * @brief Toggle visibility of Yumi, follow Yuxin Chen work
 * @version 0.1
 * @date 2023
 * 
 * @copyright Copyright Flair 2023
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YumiValueScript : MonoBehaviour
{
    private Text value;
    private GameObject target;
    public Scrollbar Arm_Slider;
    public Scrollbar Mode_Slider; 

    public bool j1 = false;
    public bool j2 = false;
    public bool j3 = false;
    public bool j4 = false;
    public bool j5 = false;
    public bool j6 = false;
    public bool j7 = false;
    private float displayValue = 0.0f;

    private GameObject[] links;
    private GameObject baseLink;
    private string routeToLink = "world/yumi_base_link/yumi_body";
    private string routeToLinkL = "world/yumi_base_link/yumi_body";

    void Start()
    {
        value = transform.Find("value").GetComponent<Text>();

        baseLink = GameObject.Find(routeToLink).gameObject;
        links = new GameObject[14];
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

        routeToLinkL += "/yumi_link_1_l";
        links[7] = GameObject.Find(routeToLinkL).gameObject;
        routeToLinkL += "/yumi_link_2_l";
        links[8] = GameObject.Find(routeToLinkL).gameObject;
        routeToLinkL += "/yumi_link_3_l";
        links[9] = GameObject.Find(routeToLinkL).gameObject;
        routeToLinkL += "/yumi_link_4_l";
        links[10] = GameObject.Find(routeToLinkL).gameObject;
        routeToLinkL += "/yumi_link_5_l";
        links[11] = GameObject.Find(routeToLinkL).gameObject;
        routeToLinkL += "/yumi_link_6_l";
        links[12] = GameObject.Find(routeToLinkL).gameObject;
        routeToLinkL += "/yumi_link_7_l";
        links[13] = GameObject.Find(routeToLinkL).gameObject;
    }

    void Update()
    {
        // define the arm (right or left)
        if (Arm_Slider.value < 0.5f)
        {
            target = GameObject.Find("world/yumi_base_link/gripper_r_controller");
            // define the mode (end-effector or joint)
            if (Mode_Slider.value < 0.5f)
            {

                if (j1)
                {
                    displayValue = target.transform.localPosition.x;
                }
                if (j2)
                {
                    displayValue = target.transform.localPosition.y;
                }
                if (j3)
                {
                    displayValue = target.transform.localPosition.z;
                }
                if (j4)
                {
                    displayValue = target.transform.eulerAngles.x;
                }
                if (j5)
                {
                    displayValue = target.transform.eulerAngles.y;
                }
                if (j6)
                {
                    displayValue = target.transform.eulerAngles.z;
                }
                displayValue = (float)(displayValue - displayValue % 0.001);
                if (value != null)

                    value.text = displayValue.ToString();
            }
            else
            {
                if (j1)
                {
                    displayValue = links[0].transform.localEulerAngles.y;
                }
                if (j2)
                {
                    displayValue = links[1].transform.localEulerAngles.x;
                }
                if (j3)
                {
                    displayValue = links[2].transform.localEulerAngles.x;
                }
                if (j4)
                {
                    displayValue = links[3].transform.localEulerAngles.x;
                }
                if (j5)
                {
                    displayValue = links[4].transform.localEulerAngles.x;
                }
                if (j6)
                {
                    displayValue = links[5].transform.localEulerAngles.x;
                }
                if (j7)
                {
                    displayValue = links[6].transform.localEulerAngles.x;
                }
                displayValue = (float)(displayValue - displayValue % 0.001);
                if (value != null)
                    value.text = displayValue.ToString();
            }
        }

        else 
        {
            target = GameObject.Find("world/yumi_base_link/gripper_l_controller");
            // define the mode (end-effector or joint)
            if (Mode_Slider.value < 0.5f)
            {
            
                if (j1)
                {
                   displayValue = target.transform.localPosition.x;
                }
                if (j2)
                {
                    displayValue = target.transform.localPosition.y;
                }
                if (j3)
                {
                    displayValue = target.transform.localPosition.z;
                }
                if (j4)
                {
                    displayValue = target.transform.eulerAngles.x;
                }
                if (j5)
                {
                    displayValue = target.transform.eulerAngles.y;
                }
                if (j6)
                {
                    displayValue = target.transform.eulerAngles.z;
                }
                displayValue = (float)(displayValue - displayValue % 0.001);
                if (value != null)

                    value.text = displayValue.ToString();
            }
            else
            {
                if (j1)
                {
                   displayValue = links[7].transform.localEulerAngles.y;
                }
                if (j2)
                {
                    displayValue = links[8].transform.localEulerAngles.x;
                }
                if (j3)
                {
                    displayValue = links[9].transform.localEulerAngles.x;
                }
                if (j4)
                {
                    displayValue = links[10].transform.localEulerAngles.x;
                }
                if (j5)
                {
                    displayValue = links[11].transform.localEulerAngles.x;
                }
                if (j6)
                {
                    displayValue = links[12].transform.localEulerAngles.x;
                }
                if (j7)
                {
                    displayValue = links[13].transform.localEulerAngles.x;
                }
                displayValue = (float)(displayValue - displayValue % 0.001);
                if (value != null)
                    value.text = displayValue.ToString();
            }
        }

        return;
    }

}
