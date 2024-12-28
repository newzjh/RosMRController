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


public class GripperButtonStateSubscriber : MonoBehaviour 
{
    [SerializeField] private string topicName = "Gripper_d";
    private ROSConnection ros;

    [SerializeField] private GameObject _gripperOpened;
    [SerializeField] private GameObject _gripperClosed;
    [SerializeField] private GameObject _gripperInactive;
    
    private bool isROSconnected = true;
    private bool isOpened = true;

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
    void Update() // Comment out commented parts for testing without connecting to ROS
    {
        // if(Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     Debug.Log("Open gripper command received!!!");

        //     _gripperOpened.SetActive(true);
        //     _gripperClosed.SetActive(false);
        //     _gripperInactive.SetActive(false);

        //     isOpened = false;
        // }

        // if (Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     Debug.Log("Close gripper command received!!!");

        //     _gripperOpened.SetActive(false);
        //     _gripperClosed.SetActive(true);
        //     _gripperInactive.SetActive(false);

        //     isOpened = true;
        // }
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

    // private void Open2Close()
    // {
    //     if (isROSconnected && isOpened)
    //     {
    //         _gripperOpened.SetActive(false);
    //         _gripperClosed.SetActive(true);
    //         isOpened = !isOpened;
    //     }
    // }

    // private void Close2Oopen()
    // {
    //     if (isROSconnected && !isOpened)
    //     {
    //         _gripperOpened.SetActive(true);
    //         _gripperClosed.SetActive(false);
    //         isOpened = !isOpened;
    //     }
    // }

    // public void TriggerOnClick (bool force = false)
    // {
    //     _buttonOff = !_buttonOff;

    //     _menu.SetActive(_buttonOff);
    // }
}