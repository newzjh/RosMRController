/**
 * @file YumiJoint7Visable.cs 
 * @author zoequ
 * @brief Control the visibility of the Joint7 button, if MODE = Joint, the Joint7 button is visiable.
 * @version 0.1
 * @date 2023
 * 
 * @copyright Copyright Flair 2023
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YumiJoint7Visible : MonoBehaviour
{
    // Start is called before the first frame update
    public Scrollbar Mode_Slider;
    public GameObject Joint7;
    void Start()
    {
        // set current gameobject as unvisible;
        Joint7.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mode_Slider.value < 0.5f)
            Joint7.SetActive(false);
        else
            Joint7.SetActive(true);
    }
}
