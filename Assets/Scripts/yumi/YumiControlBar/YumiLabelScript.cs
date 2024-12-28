/**
 * @file YumiLabelScript.cs display joint tag in Canvas 
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

public class YumiLabelScript : MonoBehaviour
{
    private Text label;

    public Scrollbar Mode_Slider; 

    public bool j1 = false;
    public bool j2 = false;
    public bool j3 = false;
    public bool j4 = false;
    public bool j5 = false;
    public bool j6 = false;
    public bool j7 = false;

    void Start()
    {
        label = transform.Find("label").GetComponent<Text>();
    }

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
                if (j7)
                {
                    label.text = "J7";
                }
            }
        }
        return;
    }
}
