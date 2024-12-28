/**
 * @file YumiHandModeControl.cs 
 * @author zoequ
 * @brief Control the control mode of Yumi arms, follow Yuxin Chen work
 * @version 0.1
 * @date 2023
 * 
 * @copyright Copyright Flair 2023
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YumiHandModeControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Scrollbar scrollbar;
    private bool isOn = false;
    // public Text modeName;
    public TextMeshProUGUI modeName;

    private void Start()
    {
        // modeName = GameObject.Find("mode").GetComponent<Text>();
        scrollbar.value = 0.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))  //ori: U key
        {
            isOn = !isOn;
            scrollbar.value = isOn ? 1f : 0f;
        }
        if (scrollbar.value < 0.5f)
        {
            modeName.text = "Cartesian";
        }
        else
        {
            modeName.text = "Joint";
        }
    }
}
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class modeControl : MonoBehaviour
// {
//     private Slider modeSlider;
//     public TextMeshProUGUI modeNameText;

//     private void Start()
//     {
//         // Bind the slider component
//         modeSlider = GetComponent<Slider>();

//         // Add a listener to the slider value change
//         modeSlider.onValueChanged.AddListener(UpdateModeName);
        
//         // Update the mode name initially
//         UpdateModeName(modeSlider.value);
//     }

//     private void UpdateModeName(float value)
//     {
//         if (value < 0.5f)
//         {
//             modeNameText.text = "Off";
//         }
//         else if (value > 0.5f)
//         {
//             modeNameText.text = "Open";
//         }
//     }
// }
