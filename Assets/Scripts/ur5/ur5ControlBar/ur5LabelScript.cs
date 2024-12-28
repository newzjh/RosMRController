/**
* @file ur5LabelScript.cs
* @author ZoeQU
* @brief Update Canvas' labels
* @version 0.1
* @date 2024-07-11
* 
* @copyright Copyright FLAIR 2024
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ur5LabelScript : MonoBehaviour
{
    private Text label;

    public Scrollbar Mode_Slider;

    public bool j1 = false;
    public bool j2 = false;
    public bool j3 = false;
    public bool j4 = false;
    public bool j5 = false;
    public bool j6 = false;

    // Start is called before the first frame update
    void Start()
    {
        label = transform.Find("label").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mode_Slider.value < 0.5f)
        {
            if (label != null)
            {
                if (j1)
                {
                    label.text = "x";
                }
                if (j2)
                {
                    label.text = "y";
                }
                if (j3)
                {
                    label.text = "z";
                }
                if (j4)
                {
                    label.text = "alpha";
                }
                if (j5)
                {
                    label.text = "gama";
                }
                if (j6)
                {
                    label.text = "beta";
                }
            }
        }
        else
        {
            if (label != null)
            {
                if (j1)
                {
                    label.text = "J1";
                }
                if (j2)
                {
                    label.text = "J2";
                }
                if (j3)
                {
                    label.text = "J3";
                }
                if (j4)
                {
                    label.text = "J4";
                }
                if (j5)
                {
                    label.text = "J5";
                }
                if (j6)
                {
                    label.text = "J6";
                }
            }
        }
        return;
    }
}
