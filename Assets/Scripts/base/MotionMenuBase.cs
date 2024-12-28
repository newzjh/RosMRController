using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MotionMenuBase : MonoBehaviour
{
    private Transform tGlobalMenu;
    private Transform tEEMenu;
    private Transform tFunctionMenu;

    // Start is called before the first frame update
    protected void Awake()
    {
        tGlobalMenu = transform.Find("Global Menu");
        tEEMenu = transform.Find("EE Menu");
        tFunctionMenu = transform.Find("Function Menu");

        if (tGlobalMenu)
        {
            var tEE_Switch = tGlobalMenu.Find("EE_switch");
            if (tEE_Switch)
            {
                var b = tEE_Switch.GetComponent<PressableButton>();
                if (b)
                    b.OnClicked.AddListener(OnEEMenuSwitch);
            }

            var tFunction_switch = tGlobalMenu.Find("Function_switch");
            if (tFunction_switch)
            {
                var b = tFunction_switch.GetComponent<PressableButton>();
                if (b)
                    b.OnClicked.AddListener(OnFunctionMenuSwitch);
            }
        }
    }

    private void OnEEMenuSwitch()
    {
        tEEMenu.gameObject.SetActive(!tEEMenu.gameObject.activeSelf);
    }

    private void OnFunctionMenuSwitch()
    {
        tFunctionMenu.gameObject.SetActive(!tFunctionMenu.gameObject.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
