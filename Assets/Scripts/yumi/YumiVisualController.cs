/**
 * @file YumiConstraintActivator.cs
 * @author zoequ
 * @brief Toggle visibility of Yumi, follow Chen Chen (maker_cc@foxmail.com) work
 * @version 0.1
 * @date 2023
 * 
 * @copyright Copyright Flair 2023
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;

public class YumiVisualController : MonoBehaviour
{
    [SerializeField] private GameObject visibilityToggleButton;
    private string routeToLinkR = "world/yumi_base_link";
    private string routeToLinkL = "world/yumi_base_link";
    private readonly string routeToVisual = "/Visuals/unnamed";
    [SerializeField] private GameObject[] linkVisualsL;
    [SerializeField] private GameObject[] linkVisualsR;
    private bool armVisible;

    // Start is called before the first frame update
    void Start()
    {
        linkVisualsR = new GameObject[11]; // number of joints
        linkVisualsL = new GameObject[10]; // number of joints


        routeToLinkR += "/yumi_body";
        linkVisualsR[0] = transform.Find(routeToLinkR + routeToVisual).gameObject;

        routeToLinkR += "/yumi_link_1_r";
        linkVisualsR[1] = transform.Find(routeToLinkR + routeToVisual).gameObject;

        routeToLinkR += "/yumi_link_2_r";
        linkVisualsR[2] = transform.Find(routeToLinkR + routeToVisual).gameObject;

        routeToLinkR += "/yumi_link_3_r";
        linkVisualsR[3] = transform.Find(routeToLinkR + routeToVisual).gameObject;

        routeToLinkR += "/yumi_link_4_r";
        linkVisualsR[4] = transform.Find(routeToLinkR + routeToVisual).gameObject;

        routeToLinkR += "/yumi_link_5_r";
        linkVisualsR[5] = transform.Find(routeToLinkR + routeToVisual).gameObject;

        routeToLinkR += "/yumi_link_6_r";
        linkVisualsR[6] = transform.Find(routeToLinkR + routeToVisual).gameObject;

        routeToLinkR += "/yumi_link_7_r";
        linkVisualsR[7] = transform.Find(routeToLinkR + routeToVisual).gameObject;

        routeToLinkR += "/gripper_r_base";
        linkVisualsR[8] = transform.Find(routeToLinkR + routeToVisual).gameObject;

        linkVisualsR[9] = transform.Find(routeToLinkR + "/gripper_r_finger_l" + routeToVisual).gameObject;
        linkVisualsR[10] = transform.Find(routeToLinkR + "/gripper_r_finger_r" + routeToVisual).gameObject;

        routeToLinkL += "/yumi_body/yumi_link_1_l";
        linkVisualsL[0] = transform.Find(routeToLinkL + routeToVisual).gameObject;

        routeToLinkL += "/yumi_link_2_l";
        linkVisualsL[1] = transform.Find(routeToLinkL + routeToVisual).gameObject;

        routeToLinkL += "/yumi_link_3_l";
        linkVisualsL[2] = transform.Find(routeToLinkL + routeToVisual).gameObject;

        routeToLinkL += "/yumi_link_4_l";
        linkVisualsL[3] = transform.Find(routeToLinkL + routeToVisual).gameObject;

        routeToLinkL += "/yumi_link_5_l";
        linkVisualsL[4] = transform.Find(routeToLinkL + routeToVisual).gameObject;

        routeToLinkL += "/yumi_link_6_l";
        linkVisualsL[5] = transform.Find(routeToLinkL + routeToVisual).gameObject;

        routeToLinkL += "/yumi_link_7_l";
        linkVisualsL[6] = transform.Find(routeToLinkL + routeToVisual).gameObject;

        routeToLinkL += "/gripper_l_base";
        linkVisualsL[7] = transform.Find(routeToLinkL + routeToVisual).gameObject;

        linkVisualsL[8] = transform.Find(routeToLinkL + "/gripper_l_finger_l" + routeToVisual).gameObject;
        linkVisualsL[9] = transform.Find(routeToLinkL + "/gripper_l_finger_r" + routeToVisual).gameObject;


        armVisible = visibilityToggleButton.GetComponent<PressableButton>().isSelected;
    }

    // Update is called once per frame
    void Update()
    {
        //if (visibilityToggleButton.GetComponent<PressableButton>().isSelected != armVisible)
        //{
        //    switch (armVisible)
        //    {
        //        case false:
        //            HideArmVisual();
        //            armVisible = true;
        //            break;

        //        case true:
        //            ActiveArmVisual();
        //            armVisible = false;
        //            break;
        //    }
        //}
    }
    private void ActiveArmVisual()
    {
        foreach (var visualObject in linkVisualsR)
        {
            visualObject.SetActive(true);
        }

        foreach (var visualObject in linkVisualsL)
        {
            visualObject.SetActive(true);
        }
    }

    private void HideArmVisual()
    {
        foreach (var visualObject in linkVisualsR)
        {
            visualObject.SetActive(false);
        }

        foreach (var visualObject in linkVisualsL)
        {
            visualObject.SetActive(false);
        }
    }
}
