using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ErrorMessage : MonoBehaviour
{
    private GameObject[] links;
    private GameObject baseLink;
    private string routeToLink = "panda_link0";
    // private TextMesh textValue;
    string errorJoint = "";
    private TextMeshProUGUI textMeshPro;
    private int sign = 0;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        

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

    // Update is called once per frame
    void Update()
    {
        // Joint 1: -166.5° to 166.5°
        // Joint 2: -101° to 101°
        // Joint 3: -166.5° to 166.5°
        // Joint 4: -176° to -4°
        // Joint 5: -166.5° to 166.5°
        // Joint 6: -1° to 215°
        // Joint 7: -166.5° to 166.5°

        // Detects if the angle of each joint is out of limits
        errorJoint = "";

        // # print(links[0].transform.localEulerAngles.y)
        
        // Debug.Log("Joint 1: " + links[0].transform.localEulerAngles.y);
        // Debug.Log("Joint 2: " + links[1].transform.localEulerAngles.x);
        // Debug.Log("Joint 3: " + links[2].transform.localEulerAngles.x);
        // Debug.Log("Joint 4: " + links[3].transform.localEulerAngles.x);
        // Debug.Log("Joint 5: " + links[4].transform.localEulerAngles.x);
        // Debug.Log("Joint 6: " + links[5].transform.localEulerAngles.x);
        // Debug.Log("Joint 7: " + links[6].transform.localEulerAngles.x);



        sign = 0;
        if (links[0].transform.localEulerAngles.y > 166.5f || links[0].transform.localEulerAngles.y < -166.5f)
        {
            errorJoint += "Joint 1, ";
            sign = 1;
        }
        if (links[1].transform.localEulerAngles.x > 101f || links[1].transform.localEulerAngles.x < -101f)
        {
            errorJoint += "Joint 2, ";
            sign = 1;
        }
        if (links[2].transform.localEulerAngles.x > 166.5f || links[2].transform.localEulerAngles.x < -166.5f)
        {
            errorJoint += "Joint 3, ";
            sign = 1;
        }
        if (links[3].transform.localEulerAngles.x > -4 || links[3].transform.localEulerAngles.x < -176f)
        {
            errorJoint += "Joint 4, ";
            sign = 1;
        }
        if (links[4].transform.localEulerAngles.x > 166.5f || links[4].transform.localEulerAngles.x < -166.5f)
        {
            errorJoint += "Joint 5, ";
            sign = 1;
        }
        if (links[5].transform.localEulerAngles.x > 215f || links[5].transform.localEulerAngles.x < -1f)
        {
            errorJoint += "Joint 6, ";
            sign = 1;
        }
        if (links[6].transform.localEulerAngles.x > 166.5f || links[6].transform.localEulerAngles.x < -166.5f)
        {
            errorJoint += "Joint 7, ";
            sign = 1;
        }
       if (sign == 1)
        {
            errorJoint = errorJoint.Remove(errorJoint.Length - 2);
        }
        else
        {
            errorJoint = "No Error";
        }

        if (errorJoint == "No Error")
            textMeshPro.text = "Error Message: " + "No Error";
        else
            textMeshPro.text = "Error Message: " + errorJoint + " is out of limits";
    }
}
