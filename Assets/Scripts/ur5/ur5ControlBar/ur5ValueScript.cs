/**
* @file ur5ValueScript.cs
* @author ZoeQU
* @brief Update Canvas' Values
* @version 0.1
* @date 2024-07-11
* 
* @copyright Copyright FLAIR 2024
**/

using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit;
using UnityEngine;
using UnityEngine.UI;

public class ur5ValueScript : MonoBehaviour
{
    private Text value;
    public GameObject target;
    public Scrollbar Mode_Slider;

    public bool j1 = false;
    public bool j2 = false;
    public bool j3 = false;
    public bool j4 = false;
    public bool j5 = false;
    public bool j6 = false;
    private float displayValue = 0.0f;

    private GameObject[] links;
    private GameObject baseLink;
    private string routeToLink = "world/base_link";


    // Start is called before the first frame update
    void Start()
    {
        value = transform.Find("value").GetComponent<Text>();

        //baseLink = transform.Find(routeToLink).gameObject;
        baseLink = GameObject.Find(routeToLink);

        links = new GameObject[6];

        routeToLink += "/shoulder_link";
        //links[0] = transform.Find(routeToLink).gameObject;
        links[0] = GameObject.Find(routeToLink);

        routeToLink += "/upper_arm_link";
        //links[1] = transform.Find(routeToLink).gameObject;
        links[1] = GameObject.Find(routeToLink);

        routeToLink += "/forearm_link";
        //links[2] = transform.Find(routeToLink).gameObject;
        links[2] = GameObject.Find(routeToLink);

        routeToLink += "/wrist_1_link";
        //links[3] = transform.Find(routeToLink).gameObject;
        links[3] = GameObject.Find(routeToLink);

        routeToLink += "/wrist_2_link";
        //links[4] = transform.Find(routeToLink).gameObject;
        links[4] = GameObject.Find(routeToLink);

        routeToLink += "/wrist_3_link";
        //links[5] = transform.Find(routeToLink).gameObject;
        links[5] = GameObject.Find(routeToLink);
    }

    // Update is called once per frame
    void Update()
    {
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

            displayValue = (float)(displayValue - displayValue % 0.001);
            if (value != null)
                value.text = displayValue.ToString();
        }
        return;
    }
}
