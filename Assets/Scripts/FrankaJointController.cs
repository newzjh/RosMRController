/**
 * @file FrankaJointController.cs
 * @author Chen Chen (maker_cc@foxmail.com)
 * @brief Visualize joint state from ros message
 * @version 0.1
 * @date 2022-09-24
 * 
 * @copyright Copyright IRM-Lab 2022
 */

using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.FrankaInterface;

public class FrankaJointController : MonoBehaviour
{
    [SerializeField] private string topicName = "/robot_state_publisher_node_1/robot_state";
    [SerializeField] private float[] jointPosDisplay = new float[7];
    private GameObject[] links;
    private GameObject baseLink;
    private string routeToLink = "world/panda_link0";
    private ROSConnection ros;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();

        baseLink = transform.Find(routeToLink).gameObject;

        links = new GameObject[7];

        routeToLink += "/panda_link1";
        links[0] = transform.Find(routeToLink).gameObject;

        routeToLink += "/panda_link2";
        links[1] = transform.Find(routeToLink).gameObject;

        routeToLink += "/panda_link3";
        links[2] = transform.Find(routeToLink).gameObject;

        routeToLink += "/panda_link4";
        links[3] = transform.Find(routeToLink).gameObject;

        routeToLink += "/panda_link5";
        links[4] = transform.Find(routeToLink).gameObject;

        routeToLink += "/panda_link6";
        links[5] = transform.Find(routeToLink).gameObject;

        routeToLink += "/panda_link7";
        links[6] = transform.Find(routeToLink).gameObject;

        ros.Subscribe<RobotStateMsg>(topicName, UpdateJointState);
    }

    // callback function when receive joint_state
    private void UpdateJointState(RobotStateMsg robotStateMsg)
    {
        float[] jointAngles = new float[7];
        for (int i = 0; i < 7; i++)
        {
            jointPosDisplay[i] = (float)robotStateMsg.q[i];
            //double deg = robotStateMsg.q[i] * Mathf.Rad2Deg;
            //jointAngles[i] = (float)deg;
            jointAngles[i] = (float)robotStateMsg.q[i] * Mathf.Rad2Deg;
            Debug.Log(jointAngles[i]);
        }

        // synatex of the rotation. Need some try, goodluck :)
        links[0].transform.rotation = baseLink.transform.rotation * Quaternion.Euler(0, -jointAngles[0], 0);
        links[1].transform.rotation = links[0].transform.rotation * Quaternion.Euler(jointAngles[1], 0, 90);
        links[2].transform.rotation = links[1].transform.rotation * Quaternion.Euler(-jointAngles[2], 0, -90);
        links[3].transform.rotation = links[2].transform.rotation * Quaternion.Euler(-jointAngles[3], 0, -90);
        links[4].transform.rotation = links[3].transform.rotation * Quaternion.Euler(jointAngles[4], 0, 90);
        links[5].transform.rotation = links[4].transform.rotation * Quaternion.Euler(-jointAngles[5], 0, -90);
        links[6].transform.rotation = links[5].transform.rotation * Quaternion.Euler(-jointAngles[6], 0, -90);
    }
}

