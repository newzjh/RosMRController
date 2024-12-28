using System;
using UnityEngine;
using UnityEngine.UI;
using RosMessageTypes.Geometry;
using RosMessageTypes.Std;
using RosMessageTypes.BuiltinInterfaces;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;

public class ReturnToHomeJointPositionPublisher : MonoBehaviour
{
    [SerializeField] private string topicName = "UI_Action_BackToHome";
    private ROSConnection ros;
    private PoseStampedMsg action_BackToHome = new PoseStampedMsg();
    private uint seq = 0;

    private bool isHolding;

    private float holdingDuration = 1.0f;
    private float secondsPassed = 0.0f;

    public GameObject target;
    public GameObject target_orientation_reference;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PoseStampedMsg>(topicName);
        isHolding = false;
        // Debug.Log(target.transform.localPosition.x + ", " + target.transform.localPosition.y + ", " + target.transform.localPosition.z + ", " + target.transform.eulerAngles.z + ", " + target.transform.eulerAngles.y + ", " + target.transform.eulerAngles.x);
    }
    void Update()
    {
        if (isHolding == false)
        {
            OnHoldEnded();
        }

        if (isHolding == true)
        {
            secondsPassed += Time.deltaTime;

            OnHold_ReturnToHome();

            if (secondsPassed > holdingDuration)
            {
                secondsPassed = 0.0f;
                isHolding = false;
            }
        }
    }
    public void OnHold_EE_ReturnToHome()
    {
        // Debug.Log(target.transform.localPosition.x + ", " + target.transform.localPosition.y + ", " + target.transform.localPosition.z + ", " + target.transform.eulerAngles.z + ", " + target.transform.eulerAngles.y + ", " + target.transform.eulerAngles.x);
        Debug.Log("Holdingggggggggggggg");
        target.transform.position = target_orientation_reference.transform.position;
        target.transform.eulerAngles = target_orientation_reference.transform.eulerAngles;
    }

    public void OnHold_ReturnToHome()
    {
        isHolding = true;
        action_BackToHome.header = new HeaderMsg(seq++, new TimeMsg(), "BackHome");
        action_BackToHome.pose.position.x = 1.0f;
        action_BackToHome.pose.position.y = 0.0f;
        action_BackToHome.pose.position.z = 0.0f;
        ros.Publish(topicName, action_BackToHome);
    }

    private void OnHoldEnded()
    {
        action_BackToHome.header = new HeaderMsg(seq++, new TimeMsg(), "BackHome");
        action_BackToHome.pose.position.x = 0.0f;
        action_BackToHome.pose.position.y = 1.0f;
        action_BackToHome.pose.position.z = 0.0f;
        ros.Publish(topicName, action_BackToHome);
    }
}
/**
 * @file JointManipulatorStatePublisher.cs
 * @author Chen Chen (maker_cc@foxmail.com)
 * @brief Publish desired velocity
 * @version 0.1
 * @date 2022-09-24
 * 
 * @copyright Copyright IRM-Lab 2022
 */


