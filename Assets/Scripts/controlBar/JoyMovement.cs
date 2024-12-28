using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RosMessageTypes.Std;
using RosMessageTypes.Geometry;
using RosMessageTypes.BuiltinInterfaces;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;

public class JoyMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float speed = 0.001f;
    private bool isJoy = true;
    private bool isBar = false;

    // private bool con_slider = true;
    private Vector3 moveDirection = Vector3.zero;

    public Scrollbar Mode_Slider;         //Mode min=0 max=1
    public Slider TX_Slider;    //PositionX min=4 max=12
    public Slider TY_Slider;    //PositionY min=-4 max=4
    public Slider TZ_Slider;    //PositionZ min=4 max=12
    public Slider RX_Slider;    //RotationX min=-90 max=90
    public Slider RY_Slider;    //RotationY min=-90 max=90
    public Slider RZ_Slider;    //RotationZ min=-90 max=90

    public GameObject target;

    private Vector3 start_pos;
    private Vector3 start_rot;

    // [SerializeField] private string topicName = "point";
    // private ROSConnection ros;
    // private uint seq = 0;

    public void MoveLeft()
    {
        moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
        if(Mode_Slider.value < 0.5f)
        {
            isJoy = true;
            isBar = false;
        }
        else
        {
            isJoy = false;
            isBar = true;
        }

    }

    public void MoveRight()
    {
        moveDirection = new Vector3(-1.0f, 0.0f, 0.0f);
        if(Mode_Slider.value < 0.5f)
        {
            isJoy = true;
            isBar = false;
        }
        else
        {
            isJoy = false;
            isBar = true;
        }

    }

    public void MoveUp()
    {
        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);
        if(Mode_Slider.value < 0.5f)
        {
            isJoy = true;
            isBar = false;
        }
        else
        {
            isJoy = false;
            isBar = true;
        }

    }

    public void MoveDown()
    {
        moveDirection = new Vector3(0.0f, -1.0f, 0.0f);
        if(Mode_Slider.value < 0.5f)
        {
            isJoy = true;
            isBar = false;
        }
        else
        {
            isJoy = false;
            isBar = true;
        }
    }

    public void MoveFront()
    {
        moveDirection = new Vector3(0.0f, 0.0f, -1.0f);
        if(Mode_Slider.value < 0.5f)
        {
            isJoy = true;
            isBar = false;
        }
        else
        {
            isJoy = false;
            isBar = true;
        }

    }

    public void MoveBack()
    {
        moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
        if(Mode_Slider.value < 0.5f)
        {
            isJoy = true;
            isBar = false;
        }
        else
        {
            isJoy = false;
            isBar = true;
        }

    }

    public void stop()
    {
        moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        if(Mode_Slider.value < 0.5f)
        {
            isJoy = false;
            isBar = false;
        }
        else
        {
            isJoy = false;
            isBar = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("OnPointerDown: " + eventData.pointerPress.gameObject.name);
        switch (eventData.pointerPress.gameObject.name)
        {
            // case "UpButton":
            //     moveDirection = Vector3.up;
            //     break;
            // case "DownButton":
            //     moveDirection = Vector3.down;
            //     break;
            // case "LeftButton":
            //     moveDirection = Vector3.left;
            //     break;
            // case "RightButton":
            //     moveDirection = Vector3.right;
            //     break;
            // case "FrontButton":
            //     moveDirection = Vector3.forward;
            //     break;
            // case "BackButton":
            //     moveDirection = Vector3.back;
            //     break;
            case "UpButton":
                MoveUp();
                break;
            case "DownButton":
                MoveDown();
                break;
            case "LeftButton":
                MoveLeft();
                break;
            case "RightButton":
                MoveRight();
                break;
            case "FrontButton":
                MoveFront();
                break;
            case "BackButton":
                MoveBack();
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stop();
    }

    

    void Start()
    {
        // assign the start position as (1,0,0)
        start_pos = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, target.transform.localPosition.z);
        start_rot = new Vector3(target.transform.eulerAngles.x, target.transform.eulerAngles.y, target.transform.eulerAngles.z);
    
        // Debug.Log("start_pos: " + start_pos);
        // Debug.Log("start_rot: " + start_rot);

        TX_Slider.value = 0;
        TY_Slider.value = 0;
        TZ_Slider.value = 0;
        RX_Slider.value = 0;
        RY_Slider.value = 0;
        RZ_Slider.value = 0;

        TX_Slider.interactable = false;
        TY_Slider.interactable = false;
        TZ_Slider.interactable = false;
        RX_Slider.interactable = false;
        RY_Slider.interactable = false;
        RZ_Slider.interactable = false;

        // ros = ROSConnection.GetOrCreateInstance();
        // ros.RegisterPublisher<PointStampedMsg>(topicName);

    }

    void Update()
    {

        //  Debug.Log("con_slider: " + con_slider);

        if(Mode_Slider.value < 0.5f)
        {
            isJoy = true && isJoy;
            isBar = false;
            TX_Slider.interactable = false;
            TY_Slider.interactable = false;
            TZ_Slider.interactable = false;
            RX_Slider.interactable = false;
            RY_Slider.interactable = false;
            RZ_Slider.interactable = false;
        }
        else
        {
            isJoy = false && isJoy;
            isBar = true;
            TX_Slider.interactable = true;
            TY_Slider.interactable = true;
            TZ_Slider.interactable = true;
            RX_Slider.interactable = true;
            RY_Slider.interactable = true;
            RZ_Slider.interactable = true;
        }

        // Debug.Log("isJoy: " + isJoy);
        // Debug.Log("isBar: " + isBar);
        // Debug.Log("start_pos: " + start_pos);
        // Debug.Log("start_rot: " + start_rot);
        
        if (isJoy) //isMoving
        {
            target.transform.localPosition += moveDirection * speed * Time.deltaTime;

            // Debug.Log("target.transform.eulerAngles.x: " + target.transform.eulerAngles.x);

            TX_Slider.value = target.transform.localPosition.x - start_pos.x;
            TY_Slider.value = target.transform.localPosition.y - start_pos.y;
            TZ_Slider.value = target.transform.localPosition.z - start_pos.z;
            if (target.transform.eulerAngles.x < 45)
            {
                RX_Slider.value = target.transform.eulerAngles.x - start_rot.x;
            }
            else
            {
                RX_Slider.value = target.transform.eulerAngles.x - start_rot.x - 360;
            }
            RY_Slider.value = target.transform.eulerAngles.y - start_rot.y;
            RZ_Slider.value = target.transform.eulerAngles.z - start_rot.z;
            // con_slider = false;
        }
        if (isBar)// Mode_Slider.value > 0.5f
        {
            float px, py, pz, rx, ry, rz;

            px = TX_Slider.value;
            py = TY_Slider.value;
            pz = TZ_Slider.value;
            rx = RX_Slider.value;
            ry = RY_Slider.value;
            rz = RZ_Slider.value;
            
            // Debug.Log("px: " + px);
            // Debug.Log("py: " + py);
            // Debug.Log("pz: " + pz);
            // Debug.Log("rx: " + rx);
            // Debug.Log("ry: " + ry);
            // Debug.Log("rz: " + rz); 

            target.transform.localPosition = new Vector3(px, py, pz) + start_pos;
            target.transform.eulerAngles = new Vector3(rx, ry, rz) + start_rot;

            // Debug.Log("localPosition: " + target.transform.localPosition);
            // Debug.Log("eulerAngles: " + target.transform.eulerAngles);
            // PointStampedMsg sourceEePositionStatemsg = new PointStampedMsg();
            // sourceEePositionStatemsg.header = new HeaderMsg(seq++, new TimeMsg(), "base");
            // sourceEePositionStatemsg.point.y = px;
            // sourceEePositionStatemsg.point.z = py;
            // sourceEePositionStatemsg.point.x = pz;

            // ros.Publish(topicName, sourceEePositionStatemsg);
        }
        // con_slider = true;
    }
}