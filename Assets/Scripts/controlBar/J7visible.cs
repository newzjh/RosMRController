using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J7visible : MonoBehaviour
{
    // Start is called before the first frame update
    public Scrollbar Mode_Slider;
    public GameObject joint7;
    void Start()
    {
        // set current gameobject as unvisible;
        joint7.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mode_Slider.value < 0.5f)
            joint7.SetActive(false);
        else
            joint7.SetActive(true);
    }
}
