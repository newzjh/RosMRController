/**
 * @file PandaVisualController.cs
 * @author Chen Chen (maker_cc@foxmail.com)
 * @brief Toggle visibility of franka
 * @version 0.1
 * @date 2022-09-24
 * 
 * @copyright Copyright IRM-Lab 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;
using RootMotion.FinalIK;



public class PandaVisualController : MonoBehaviour
{
    [SerializeField] private GameObject visibilityToggleButton;
    private string routeToLink = "world";
    private readonly string routeToVisual = "/Visuals/unnamed";
    [SerializeField]private GameObject[] linkVisuals;
    private bool armVisible;

    void Start()
    {
        linkVisuals = new GameObject[11];
        routeToLink += "/panda_link0";
        linkVisuals[0] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/panda_link1";
        linkVisuals[1] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/panda_link2";
        linkVisuals[2] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/panda_link3";
        linkVisuals[3] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/panda_link4";
        linkVisuals[4] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/panda_link5";
        linkVisuals[5] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/panda_link6";
        linkVisuals[6] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/panda_link7";
        linkVisuals[7] = transform.Find(routeToLink + routeToVisual).gameObject;

        routeToLink += "/panda_link8/panda_hand";
        linkVisuals[8] = transform.Find(routeToLink + routeToVisual).gameObject;

        linkVisuals[9] = transform.Find(routeToLink + "/panda_leftfinger" + routeToVisual).gameObject;
        linkVisuals[10] = transform.Find(routeToLink + "/panda_rightfinger" + routeToVisual).gameObject;

        //armVisible = visibilityToggleButton.GetComponent<PressableButton>().isSelected;
    }

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

    public void SwitchEnable()
    {
        var fabrik = GetComponent<FABRIK>();
        if (!fabrik)
            return;

        fabrik.enabled = !fabrik.enabled;

        bool e = !fabrik.enabled;

        var bcs = GetComponentsInChildren<BoundsControl>(true);
        if (bcs!=null)
        {
            foreach(var bc in bcs)
            {
                bc.enabled = e;
            }
        }

        //var omes = GetComponentsInChildren<ObjectManipulatorEx>(true);
        //if (omes!=null)
        //{
        //    foreach(var ome in omes)
        //    {
        //        ome.enabled = e;
        //    }
        //}
    }
}
