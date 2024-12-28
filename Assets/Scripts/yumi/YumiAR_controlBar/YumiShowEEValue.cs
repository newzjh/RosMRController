/**
 * @file YumiShowEEValue.cs
 * @author zoequ
 * @brief this file is for show the ee value in HandMenu(Left).
 * @version 1.0
 * @date 2024
 * 
 * @copyright Flair 2024
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class YumiShowEEValue : MonoBehaviour
{
    private GameObject target;

    [SerializeField] private TMP_Text textObject;
    private TMP_Text[] m_TextComponentEE = new TMP_Text[6];
    private float[] displayEEValue = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

    // Start is called before the first frame update
    void Start()
    {
        m_TextComponentEE[0] = transform.Find("X").GetComponentInChildren<TMP_Text>(true);
        m_TextComponentEE[1] = transform.Find("Y").GetComponentInChildren<TMP_Text>(true);
        m_TextComponentEE[2] = transform.Find("Z").GetComponentInChildren<TMP_Text>(true);
        m_TextComponentEE[3] = transform.Find("alpha").GetComponentInChildren<TMP_Text>(true);
        m_TextComponentEE[4] = transform.Find("beta").GetComponentInChildren<TMP_Text>(true);
        m_TextComponentEE[5] = transform.Find("gamma").GetComponentInChildren<TMP_Text>(true);
    }


    private void GetEEValue()
    {
        displayEEValue[0] = -target.transform.localPosition.x; // adding negative sign so display value increase on forward, more intuitive
        displayEEValue[1] = target.transform.localPosition.y;
        displayEEValue[2] = target.transform.localPosition.z;
        displayEEValue[3] = target.transform.eulerAngles.z;
        displayEEValue[4] = target.transform.eulerAngles.y;
        displayEEValue[5] = target.transform.eulerAngles.x;
    }

    private void EEValueToText(int listOrderOfDisplayValue)
    {
        displayEEValue[listOrderOfDisplayValue] = (float)((displayEEValue[listOrderOfDisplayValue] - displayEEValue[listOrderOfDisplayValue] % 0.001));
        m_TextComponentEE[listOrderOfDisplayValue].text = displayEEValue[listOrderOfDisplayValue].ToString();
    }


    // Update is called once per frame
    void Update()
    {
        if (YumiMotion_ARControlBase.Instance)
        {
            target = YumiMotion_ARControlBase.Instance.gameObject;
        }
        else
        {
            if (textObject != null && textObject.text == "Right Arm")
            {
                var t = GameObject.Find("world/yumi_base_link/gripper_r_controller");
                if (t)
                    target = t.gameObject;
            }
            else
            {
                var t = GameObject.Find("world/yumi_base_link/gripper_l_controller");
                if (t)
                    target = t.gameObject;
            }
        }

        if (target == null)
            return;

        GetEEValue();
        for (int i = 0; i < 6; i++)
        {
            EEValueToText(i);
        }
    }
}
