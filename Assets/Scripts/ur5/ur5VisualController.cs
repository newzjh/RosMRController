/**
* @file ur5VisualController.cs
* @author ZoeQU
* @brief Toggle visibility of ur5
* @version 0.1
* @date 2024-07-11
* 
* @copyright Copyright Flair 2024
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;

public class ur5VisualController : MonoBehaviour
{
    [SerializeField] private GameObject visibilityToggleButton;
    private string routeToLink = "world";
    private readonly string routeToVisual = "/Visuals/unnamed";
    private GameObject[] linkVisuals;
    private bool armVisible;

    // Start is called before the first frame update
    void Start()
    {
        linkVisuals = new GameObject[16];

        routeToLink += "/base_link";
        linkVisuals[0] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/shoulder_link";
        linkVisuals[1] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/upper_arm_link";
        linkVisuals[2] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/forearm_link";
        linkVisuals[3] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/wrist_1_link";
        linkVisuals[4] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/wrist_2_link";
        linkVisuals[5] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/wrist_3_link";
        linkVisuals[6] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/robotiq_85_base_link";
        linkVisuals[7] = transform.Find(routeToLink + routeToVisual).gameObject;

        linkVisuals[8] = transform.Find(routeToLink + "/robotiq_85_right_inner_knuckle_link" + routeToVisual).gameObject;
        linkVisuals[9] = transform.Find(routeToLink + "/robotiq_85_right_inner_knuckle_link/robotiq_85_right_finger_tip_link" + routeToVisual).gameObject;

        linkVisuals[10] = transform.Find(routeToLink + "/robotiq_85_left_inner_knuckle_link" + routeToVisual).gameObject;
        linkVisuals[11] = transform.Find(routeToLink + "/robotiq_85_left_inner_knuckle_link/robotiq_85_left_finger_tip_link" + routeToVisual).gameObject;

        linkVisuals[12] = transform.Find(routeToLink + "/robotiq_85_right_knuckle_link" + routeToVisual).gameObject;
        linkVisuals[13] = transform.Find(routeToLink + "/robotiq_85_right_knuckle_link/robotiq_85_right_finger_link" + routeToVisual).gameObject;

        linkVisuals[14] = transform.Find(routeToLink + "/robotiq_85_left_knuckle_link" + routeToVisual).gameObject;
        linkVisuals[15] = transform.Find(routeToLink + "/robotiq_85_left_knuckle_link/robotiq_85_left_finger_link" + routeToVisual).gameObject;

        
        armVisible = visibilityToggleButton.GetComponent<PressableButton>().isSelected;
    }

    // Update is called once per frame
    void Update()
    {
        if (visibilityToggleButton.GetComponent<PressableButton>().isSelected != armVisible)
        {
            switch (armVisible)
            {
                case false:
                    HideArmVisual();
                    armVisible = true;
                    break;

                case true:
                    ActiveArmVisual();
                    armVisible = false;
                    break;
            }
        }
    }

    private void ActiveArmVisual()
    {
        foreach (var visualObject in linkVisuals)
        {
            visualObject.SetActive(true);
        }
    }

    private void HideArmVisual()
    {
        foreach (var visualObject in linkVisuals)
        {
            visualObject.SetActive(false);
        }
    }
    
}
