/**
 * @file YumiJointCoordinate.cs 
 * @author zoequ
 * @brief Print/find all Yumi joints' & Manipulation Cube's coordinate
 * @version 0.1
 * @date 2023
 * 
 * @copyright Copyright Flair 2023
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class YumiJointCoordinate : MonoBehaviour
{
    private GameObject Cube;
    private GameObject Links;
    // Start is called before the first frame update

    void CoordinateTransform()
    { 
    }

    void Start()
    {
        Cube = GameObject.Find("YumiPosManipulationCube").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        print("Cube Coordinate: " + Cube.transform.localPosition);
    }
}
