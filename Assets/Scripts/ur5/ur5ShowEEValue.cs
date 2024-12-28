using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ur5ShowEEValue : MonoBehaviour
{
    private TMP_Text[] m_TextComponentEE = new TMP_Text[6];
    private float[] displayEEValue = new float[6] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public GameObject target;

    private int valueDisplayToTenthPlace = 100;

    // Start is called before the first frame update
    void Start()
    {
        m_TextComponentEE[0] = transform.Find("X").GetComponent<TMP_Text>();
        m_TextComponentEE[1] = transform.Find("Y").GetComponent<TMP_Text>();
        m_TextComponentEE[2] = transform.Find("Z").GetComponent<TMP_Text>();
        m_TextComponentEE[3] = transform.Find("alpha").GetComponent<TMP_Text>();
        m_TextComponentEE[4] = transform.Find("beta").GetComponent<TMP_Text>();
        m_TextComponentEE[5] = transform.Find("gamma").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        GetEEValue();
        for (int i = 0; i < 6; i++)
        {
            EEValueToText(i);
        }
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
}
