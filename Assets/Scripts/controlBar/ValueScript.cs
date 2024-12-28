using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueScript : MonoBehaviour
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
    public bool j7 = false;
    private float displayValue = 0.0f;

    private GameObject[] links;
    private GameObject baseLink;
    private string routeToLink = "panda_link0";

    void Start()
    {
        value = transform.Find("value").GetComponent<Text>();


        baseLink = GameObject.Find(routeToLink).gameObject;
        links = new GameObject[7];
        routeToLink += "/panda_link1";
        links[0] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link2";
        links[1] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link3";
        links[2] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link4";
        links[3] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link5";
        links[4] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link6";
        links[5] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link7";
        links[6] = GameObject.Find(routeToLink).gameObject;
    }

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
            if (j7)
            {
                displayValue = links[6].transform.localEulerAngles.x;
            }
            displayValue = (float)(displayValue - displayValue % 0.001);
            if (value != null)
                value.text = displayValue.ToString();
        }
        return;
    }
}
