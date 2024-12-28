/**
* @file ur5JointController.cs
* @author ZoeQU
* @brief Visualize ur5 joint state from ros message
* @version 0.1
* @date 2024-07-11
* 
* @copyright Copyright FLAIR 2024
**/

/**
This script subscribes to ROS topic "/robot_state_publisher_node_1/robot_state" to obtain q (joint values 7x1 array)
then instandly remote update changes from real arm to Unity model.

With this active in the Unity scene, the positioin of the Unity model will follow the real arm completely, meaning that
no movement changes could be applied to the Unity model
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.FrankaInterface;

public class ur5JointController : MonoBehaviour
{
    [SerializeField] private string topicName = "/robot_state_publisher_node_1/robot_state"; //
    [SerializeField] public float[] jointPosDisplay = new float[6];
    private GameObject[] links;
    private GameObject baseLink;
    private string routeToLink = "world/base_link";
    private ROSConnection ros;

    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();

        baseLink = transform.Find(routeToLink).gameObject;

        links = new GameObject[6];

        routeToLink += "/shoulder_link";
        links[0] = transform.Find(routeToLink).gameObject;

        routeToLink += "/upper_arm_link";
        links[1] = transform.Find(routeToLink).gameObject;

        routeToLink += "/forearm_link";
        links[2] = transform.Find(routeToLink).gameObject;

        routeToLink += "/wrist_1_link";
        links[3] = transform.Find(routeToLink).gameObject;

        routeToLink += "/wrist_2_link";
        links[4] = transform.Find(routeToLink).gameObject;

        routeToLink += "/wrist_3_link";
        links[5] = transform.Find(routeToLink).gameObject;

        //routeToLink += "/robotiq_85_base_link";
        //links[6] = transform.Find(routeToLink).gameObject;

        ros.Subscribe<RobotStateMsg>(topicName, UpdateJointState);
    }

    // callback function when receive joint_state
    private void UpdateJointState(RobotStateMsg robotStateMsg)
    {
        float[] jointAngles = new float[6];
        for (int i = 0; i < 6; i++)
        {
            jointPosDisplay[i] = (float)robotStateMsg.q[i];
            //double deg = robotStateMsg.q[i] * Mathf.Rad2Deg;
            //jointAngles[i] = (float)deg;
            jointAngles[i] = (float)robotStateMsg.q[i] * Mathf.Rad2Deg;
        }

        // synatex of the rotation. Need some try, goodluck :)
        links[0].transform.rotation = baseLink.transform.rotation * Quaternion.Euler(0, -jointAngles[0], 0);
        links[1].transform.rotation = links[0].transform.rotation * Quaternion.Euler(jointAngles[1], 0, 90);
        links[2].transform.rotation = links[1].transform.rotation * Quaternion.Euler(-jointAngles[2], 0, -90);
        links[3].transform.rotation = links[2].transform.rotation * Quaternion.Euler(-jointAngles[3], 0, -90);
        links[4].transform.rotation = links[3].transform.rotation * Quaternion.Euler(jointAngles[4], 0, 90);
        links[5].transform.rotation = links[4].transform.rotation * Quaternion.Euler(-jointAngles[5], 0, -90);
        //links[6].transform.rotation = links[5].transform.rotation * Quaternion.Euler(-jointAngles[6], 0, -90);
    }
}
