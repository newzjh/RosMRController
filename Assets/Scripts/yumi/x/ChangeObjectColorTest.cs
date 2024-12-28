using System.Collections;
using System.Collections.Generic;
using Unity.Robotics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeObjectColorTest : MonoBehaviour
{
    // add a state, when select the joint, change its color;
    public Color highLightColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void change()
        {
        // change the color of the selected GameObject
        GameObject jointObject = GameObject.Find("panda_link0").gameObject;
        Renderer renderer = jointObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            MaterialExtensions.SetMaterialColor(renderer.material, highLightColor);
            print("clicked!");
        }
        else
        {
            print("No renderer!");
        }
    }

}
