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

public class motion_ARControl: MonoBehaviour
{
    public float speed = 0.01f;
    public float rotation_speed = 1;
    private bool isJoint = false;
    private bool isEuclid = true;

    private bool translation = false;
    private bool rotation = false;

    // private bool con_slider = true;
    private Vector3 moveDirection = Vector3.zero;
    // set jointDirection as a 7 dimension float array;
    private float[] jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    

    public Scrollbar Mode_Slider;         //Mode min=0 max=1

    public GameObject target;

    private GameObject[] links;
    private GameObject baseLink;
    private string routeToLink = "panda_link0";

    private Vector3 start_pos;
    private Vector3 start_rot;

    private bool TrailVisualable = true;
    // [SerializeField] private string topicName = "point";
    // private ROSConnection ros;
    // private uint seq = 0;

    // ------------------------------------------------------------------------------
    public void moveEuc1Minus()
    {
        Debug.Log("moveEuc1Minus");
        translation = true;
        rotation = false;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[7]{-1.0f, 0.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }
    public void moveEuc1Plus()
    {
        Debug.Log("moveEuc1Plus");
        translation = true;
        rotation = false;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[7]{1.0f, 0.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }

    public void moveEuc2Minus()
    {
        Debug.Log("moveEuc2Minus");
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[7]{0.0f, -1.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }
    public void moveEuc2Plus()
    {
        Debug.Log("moveEuc2Plus");
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 1.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }

    public void moveEuc3Minus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[7]{0.0f, 0.0f, -1.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }
    public void moveEuc3Plus()
    {
        translation = true;
        rotation = false;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 1.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }

    public void moveEuc4Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, -1.0f, 0.0f ,0.0f, 0.0f};
    }
    public void moveEuc4Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 1.0f, 0.0f ,0.0f, 0.0f};
    }

    public void moveEuc5Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, -1.0f ,0.0f, 0.0f};
    }
    public void moveEuc5Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 1.0f ,0.0f, 0.0f};
    }

    public void moveEuc6Minus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,-1.0f, 0.0f};
    }
    public void moveEuc6Plus()
    {
        translation = false;
        rotation = true;
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,1.0f, 0.0f};
    }
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    public void moveJ7Minus()
    {
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,0.0f, -1.0f};
    }
    public void moveJ7Plus()
    {
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,0.0f, 1.0f};
    }

    // ------------------------------------------------------------------------------
    public void stop()
    {
        moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        jointDirection = new float[7]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f ,0.0f, 0.0f};
    }

    // public bool movePositiveX = false;
    // public bool moveNegativeX = false;


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
    }

    void Update()
    {

        // Debug.Log("update with direction" + moveDirection);
        // Debug.Log("uMode_Slider.value" + Mode_Slider.value);
        if(Mode_Slider.value < 0.5f)
        {
            isEuclid = true;
            isJoint = false;
        }
        else
        {
            isEuclid = false;
            isJoint = true;
        }

        // Debug.Log("isJoy: " + isJoy);
        // Debug.Log("isBar: " + isBar);
        // Debug.Log("start_pos: " + start_pos);
        // Debug.Log("start_rot: " + start_rot);
        
        if (isEuclid) //isMoving
        {
            // Debug.Log("isEuclid");
            if (translation)
            {
                target.transform.localPosition += moveDirection * speed * Time.deltaTime;
            }
            else if (rotation)
            {
                target.transform.eulerAngles += moveDirection * rotation_speed * 10 * Time.deltaTime;
            }

            // Debug.Log("target.transform.eulerAngles.x: " + target.transform.eulerAngles.x);

            // TX_Slider.value = target.transform.localPosition.x - start_pos.x;
            // TY_Slider.value = target.transform.localPosition.y - start_pos.y;
            // TZ_Slider.value = target.transform.localPosition.z - start_pos.z;
            // con_slider = false;
        }
        if (isJoint)// Mode_Slider.value > 0.5f
        {

            // Debug.Log("isJoint");
            for(int i = 0; i < 7; i++)
            {
                if(jointDirection[i] == 0.0f)
                    continue;
                if(i == 0)
                    links[i].transform.localEulerAngles += new Vector3(0.0f, jointDirection[i], 0.0f) * speed * 100 * Time.deltaTime;
                else
                    links[i].transform.localEulerAngles += new Vector3(jointDirection[i], 0.0f, 0.0f)  * speed * 100 * Time.deltaTime;
            }
            // float px, py, pz, rx, ry, rz;
            // px = TX_Slider.value;
            // py = TY_Slider.value;
            // pz = TZ_Slider.value;
            // rx = RX_Slider.value;
            // ry = RY_Slider.value;
            // rz = RZ_Slider.value;
            // Debug.Log("px: " + px);
            // Debug.Log("py: " + py);
            // Debug.Log("pz: " + pz);
            // Debug.Log("rx: " + rx);
            // Debug.Log("ry: " + ry);
            // Debug.Log("rz: " + rz);
            // target.transform.localPosition = new Vector3(px, py, pz) + start_pos;
            // target.transform.eulerAngles = new Vector3(rx, ry, rz) + start_rot;
            // Debug.Log("localPosition: " + target.transform.localPosition);
            // Debug.Log("eulerAngles: " + target.transform.eulerAngles);
            // PointStampedMsg sourceEePositionStatemsg = new PointStampedMsg();
            // sourceEePositionStatemsg.header = new HeaderMsg(seq++, new TimeMsg(), "base");
            // sourceEePositionStatemsg.point.y = px;
            // sourceEePositionStatemsg.point.z = py;
            // sourceEePositionStatemsg.point.x = pz;

            // ros.Publish(topicName, sourceEePositionStatemsg);
        }
    }
}