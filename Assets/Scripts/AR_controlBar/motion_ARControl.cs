using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RosMessageTypes.Std;
using RosMessageTypes.Geometry;
using RosMessageTypes.BuiltinInterfaces;
using MixedReality.Toolkit;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;
// using Unity.Robotics.ROSTCPConnector;
// using Unity.Robotics.ROSTCPConnector.ROSGeometry;

public class motion_ARControl: Motion_ARControlBase
{



    



    private string routeToLink = "panda_link0";

    private Vector3 start_pos;
    private Vector3 start_rot;

    private bool TrailVisualable = true;
    // [SerializeField] private string topicName = "point";
    // private ROSConnection ros;
    // private uint seq = 0;

 


    public void FrankaControlTrailRenderer()
    {
        if (TrailVisualable)
        {
            TrailRenderer trailRenderer = GameObject.Find("panda_hand_controller").GetComponent<TrailRenderer>();
            trailRenderer.enabled = true;
            TrailVisualable = false;

        }
        else
        {
            TrailRenderer trailRenderer = GameObject.Find("panda_hand_controller").GetComponent<TrailRenderer>();
            trailRenderer.enabled = false;
            TrailVisualable = true;
        }
    }


    void Start()
    {
        // start_pos = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, target.transform.localPosition.z);
        // start_rot = new Vector3(target.transform.eulerAngles.x, target.transform.eulerAngles.y, target.transform.eulerAngles.z);
        // ros = ROSConnection.GetOrCreateInstance();
        // ros.RegisterPublisher<PointStampedMsg>(topicName);
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

        target = transform.gameObject;
    }

}