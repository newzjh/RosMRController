/**
 * @file YumiPositionFollow.cs
 * @author zoequ
 * @brief Yumi follow the manipulation cube, follow Chen Chen (maker_cc@foxmail.com) work
 * @version 0.1
 * @date 2023
 * 
 * @copyright Copyright Flair 2023
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using Microsoft.MixedReality.Toolkit;
[CustomEditor(typeof(YumiPositionFollow2))]
[CanEditMultipleObjects]
public class YumiPositionFollow2Inspector : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        YumiPositionFollow2 follow2 = target as YumiPositionFollow2;

        if (GUILayout.Button("clear"))
        {
            PlayerPrefs.SetString("YumiPosition", string.Empty);
            PlayerPrefs.SetString("YumiRotation", string.Empty);
        }

        base.OnInspectorGUI();
    }
}

#endif

public class YumiPositionFollow2 : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    private Vector3 relativePos;
    private Quaternion relativeRot;

    // Start is called before the first frame update
    void Start()
    {
        relativePos = transform.position - targetObject.transform.position;
        relativeRot = transform.rotation * Quaternion.Inverse(targetObject.transform.rotation);

        if (Application.isPlaying)
        {
            string strPos = PlayerPrefs.GetString("YumiPosition");
            string strRotation = PlayerPrefs.GetString("YumiRotation");
            if (!string.IsNullOrEmpty(strPos) && !string.IsNullOrEmpty(strRotation))
            {
                Vector3 pos = JsonUtility.FromJson<Vector3>(strPos);
                Quaternion q = JsonUtility.FromJson<Quaternion>(strRotation);
                transform.rotation = q;
                transform.position = pos;

                targetObject.transform.rotation = transform.rotation * Quaternion.Inverse(relativeRot);
                targetObject.transform.position = transform.position - relativePos;


                Debug.Log("recover Yumi position and rotation!");
            }
        }

    }

    Vector3 lastpos = Vector3.zero;
    Quaternion lastq = Quaternion.identity;

    // Update is called once per frame
    void Update()
    {
        float targetRotY = targetObject.transform.rotation.eulerAngles.y;
        Vector3 pos = targetObject.transform.position + relativePos;
        Quaternion q = Quaternion.Euler(0, targetRotY, 0) * relativeRot;
        transform.SetPositionAndRotation(
            pos,
            q);

        if (Application.isPlaying)
        {
            if (pos != lastpos || q != lastq)
            {
                string strPos = JsonUtility.ToJson(pos);
                string strRotation = JsonUtility.ToJson(q);
                PlayerPrefs.SetString("YumiPosition", strPos);
                PlayerPrefs.SetString("YumiRotation", strRotation);

                lastpos = pos;
                lastq = q;
            }
        }
    }
}
