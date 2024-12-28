/**
 * @file ur5EndEffectorPublisher.cs
 * @author ZoeQU
 * @brief Subscribe gripper state
 * @version 0.1
 * @date 2024-07-11
 * 
 * @copyright Copyright Flair 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Std;
using RosMessageTypes.Geometry;
using RosMessageTypes.BuiltinInterfaces;

public class ur5ButtonStateSubscriber : MonoBehaviour
{
    [SerializeField] private string topicName = "Gripper_d";
    private ROSConnection ros;

    [SerializeField] private GameObject _gripperOpened;
    [SerializeField] private GameObject _gripperClosed;
    [SerializeField] private GameObject _gripperInactive;

    private bool isROSconnected = true;
    private bool isOpened = true;

    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<PoseStampedMsg>(topicName, UpdateButtonState);

        // _gripperOpened.SetActive(false);
        // _gripperOpened.SetActive(true);
        // _gripperClosed.SetActive(false);
        // _gripperInactive.SetActive(false);
    }

    void OnEnable()
    {
        if (isROSconnected == false)
        {
            _gripperOpened.SetActive(false);
            _gripperClosed.SetActive(false);
            _gripperInactive.SetActive(true);
        }
        if (isROSconnected == true && isOpened == true)
        {
            _gripperOpened.SetActive(true);
            _gripperClosed.SetActive(false);
            _gripperInactive.SetActive(false);
        }

        if (isROSconnected == true && isOpened == false)
        {
            _gripperOpened.SetActive(false);
            _gripperClosed.SetActive(true);
            _gripperInactive.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateButtonState(PoseStampedMsg poseStampedMsg) // Comment out when not connected to ROS
    {
        if (ros.HasConnectionError)
        {
            _gripperInactive.SetActive(true);
            _gripperOpened.SetActive(false);
            _gripperClosed.SetActive(false);
            isROSconnected = false;
        }

        else
        {
            if (poseStampedMsg.pose.position.x != 0.0) // Condition when gripper opened
            {
                // Debug.Log("Opened");
                _gripperOpened.SetActive(true);
                _gripperClosed.SetActive(false);
                _gripperInactive.SetActive(false);
                isROSconnected = true;
                isOpened = true;
            }
            else // Else: Condition when gripper closed
            {
                // Debug.Log("Closed");
                _gripperOpened.SetActive(false);
                _gripperClosed.SetActive(true);
                _gripperInactive.SetActive(false);
                isROSconnected = true;
                isOpened = false;
            }
        }
    }

}
