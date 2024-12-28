/**
 * @file YumiHandSwitch.cs 
 * @author zoequ
 * @brief Switch the controlled hand of Yumi
 * @version 0.1
 * @date 2023
 * 
 * @copyright Copyright Flair 2023
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YumiHandSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public Scrollbar scrollbar;
    private bool isOn = false;
    // public Text modeName;
    public TextMeshProUGUI modeName;

    void Start()
    {
        // modeName = GameObject.Find("mode").GetComponent<Text>();
        scrollbar.value = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            isOn = !isOn;
            scrollbar.value = isOn ? 1f : 0f;
        }
        if (scrollbar.value < 0.5f)
        {
            modeName.text = "Right";
            // Debug.Log("Right Arm");
        }
        else
        {
            modeName.text = "Left";
            // Debug.Log("Left Arm");
        }
    }
}

