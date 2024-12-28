using UnityEngine;
using TMPro;
 
public class ShowJointsValue : MonoBehaviour
{
    private TMP_Text[] m_TextComponentJoint = new TMP_Text[7];
    private float[] displayJointValue = new float[7] {0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    public GameObject target;

    private GameObject[] links;
    private GameObject baseLink;
    private string routeToLink = "panda_link0";

 
    void Start()
    {
        m_TextComponentJoint[0] = transform.Find("J1").GetComponent<TMP_Text>();
        m_TextComponentJoint[1] = transform.Find("J2").GetComponent<TMP_Text>();
        m_TextComponentJoint[2] = transform.Find("J3").GetComponent<TMP_Text>();
        m_TextComponentJoint[3] = transform.Find("J4").GetComponent<TMP_Text>();
        m_TextComponentJoint[4] = transform.Find("J5").GetComponent<TMP_Text>();
        m_TextComponentJoint[5] = transform.Find("J6").GetComponent<TMP_Text>();
        m_TextComponentJoint[6] = transform.Find("J7").GetComponent<TMP_Text>();

        baseLink = GameObject.Find(routeToLink).gameObject;
        links = new GameObject[7];
        routeToLink += "/panda_link1";
        links[0] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link2";
        links[1] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link3";
        links[2] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link4";
        links[3] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link5";
        links[4] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link6";
        links[5] = GameObject.Find(routeToLink).gameObject;
        routeToLink += "/panda_link7";
        links[6] = GameObject.Find(routeToLink).gameObject;
    }
    // void OnEnable()
    // {
    //     GetJointValue();
    //     for (int i = 0; i <= 6; i++)
    //     {
    //         ValueToText(i);
    //     }
    // }

    void Update() 
    {
        GetJointValue();
        for (int i = 0; i <= 6; i++)
        {
            JointValueToText(i);
        }
    }

    // private void GetEEValue()
    // {
    //     displayEEValue[0] = target.transform.localPosition.x;
    //     displayEEValue[1] = target.transform.localPosition.y;
    //     displayEEValue[2] = target.transform.localPosition.z;
    //     displayEEValue[3] = target.transform.eulerAngles.x;
    //     displayEEValue[4] = target.transform.eulerAngles.y;
    //     displayEEValue[5] = target.transform.eulerAngles.z;
    // }

    private void GetJointValue()
    {
        displayJointValue[0] = links[0].transform.localEulerAngles.y;
        displayJointValue[1] = links[1].transform.localEulerAngles.x;
        displayJointValue[2] = links[2].transform.localEulerAngles.x;
        displayJointValue[3] = links[3].transform.localEulerAngles.x;
        displayJointValue[4] = links[4].transform.localEulerAngles.x;
        displayJointValue[5] = links[5].transform.localEulerAngles.x;
        displayJointValue[6] = links[6].transform.localEulerAngles.x;
    }

    private void JointValueToText(int listOrderOfDisplayValue)
    {
        displayJointValue[listOrderOfDisplayValue] = (float)(displayJointValue[listOrderOfDisplayValue] - displayJointValue[listOrderOfDisplayValue] % 0.001);
        m_TextComponentJoint[listOrderOfDisplayValue].text = displayJointValue[listOrderOfDisplayValue].ToString();
    }
}