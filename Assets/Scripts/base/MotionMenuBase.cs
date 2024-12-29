using AsImpL.Examples;
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
    private Transform tJointOperationMenu;

    // Start is called before the first frame update
    protected void Start()
    {
        tGlobalMenu = transform.Find("Global Menu");
        tEEMenu = transform.Find("EE Menu");
        tFunctionMenu = transform.Find("Function Menu");
        tJointOperationMenu = transform.Find("Joint Operation Menu");

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

        Motion_ARControlBase motion = Motion_ARControlBase.Instance;

        if (tJointOperationMenu && motion)
        {
            PressableButton[] bs = tJointOperationMenu.GetComponentsInChildren<PressableButton>(true);
            if (bs!=null)
            {
                bs[0].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc5Plus();
                });
                bs[1].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc5Minus();
                });
                bs[2].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc2Plus();
                });
                bs[3].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc2Minus();
                });

                bs[4].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc4Minus();
                });
                bs[5].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc4Plus();
                });
                bs[6].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc1Plus();
                });
                bs[7].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc1Minus();
                });

                bs[8].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc6Minus();
                });
                bs[9].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc6Plus();
                });
                bs[10].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc3Plus();
                });
                bs[11].firstSelectEntered.AddListener(delegate
                {
                    motion.moveEuc3Minus();
                });

                for (int i = 0; i < 12; i++)
                {
                    bs[i].lastSelectExited.AddListener(delegate
                    {
                        motion.stop();
                    });
                }
            }
        }

        if (tFunctionMenu)
        {
            PressableButton[] bs = tFunctionMenu.GetComponentsInChildren<PressableButton>(true);
            if (bs != null)
            {
                bs[0].OnClicked.AddListener(delegate
                {
                    var importer=GameObject.FindFirstObjectByType<CustomObjImporter>(FindObjectsInactive.Include);
                    if (importer)
                        importer.HideAllMesh();
                });
                bs[1].OnClicked.AddListener(delegate
                {
                    var headtracker = GameObject.FindFirstObjectByType<HeadTracker>(FindObjectsInactive.Include);
                    if (headtracker)
                        headtracker.CalibrateZero();
                });
                bs[2].OnClicked.AddListener(delegate
                {
                    var yumimotion = motion as YumiMotion_ARControl;
                    if (yumimotion)
                        yumimotion.controlTrailRenderer();
                });
                bs[3].OnClicked.AddListener(delegate
                {
                    var importer = GameObject.FindFirstObjectByType<CustomObjImporter>(FindObjectsInactive.Include);
                    if (importer)
                        importer.Reload();
                });
                bs[4].OnClicked.AddListener(delegate
                {
                    var headtracker = GameObject.FindFirstObjectByType<HeadTracker>(FindObjectsInactive.Include);
                    if (headtracker)
                        headtracker.gameObject.SetActive(!headtracker.gameObject.activeSelf);
                });
                bs[5].OnClicked.AddListener(delegate
                {
                    var headtracker = GameObject.FindFirstObjectByType<HeadTracker>(FindObjectsInactive.Include);
                    if (headtracker)
                        headtracker.gameObject.SetActive(!headtracker.gameObject.activeSelf);
                });
                bs[6].OnClicked.AddListener(delegate
                {
                    var yumimotion = motion as YumiMotion_ARControl;
                    if (yumimotion)
                        yumimotion.goHome();
                });
                bs[7].OnClicked.AddListener(delegate
                {
                    var headtracker = GameObject.FindFirstObjectByType<HeadTracker>(FindObjectsInactive.Include);
                    if (headtracker)
                        headtracker.gameObject.SetActive(!headtracker.gameObject.activeSelf);
                });
                bs[8].OnClicked.AddListener(delegate
                {
                    var headtracker = GameObject.FindFirstObjectByType<HeadTracker>(FindObjectsInactive.Include);
                    if (headtracker)
                        headtracker.gameObject.SetActive(!headtracker.gameObject.activeSelf);
                });
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
