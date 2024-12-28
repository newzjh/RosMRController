/**
 * @file YumiFunctioMenuSetActive.cs
 * @author zoequ
 * @brief Control the status of FunctionMenu and EEValueMenu inside HandMenu (left).
 * @version 1.0
 * @date May 2024
 * 
 * @copyright Flair
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YumiFunctioMenuSetActive : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    public bool _buttonOff = true;

    // Start is called before the first frame update
    void Start()
    {
        _menu.SetActive(_buttonOff);   
    }

    public void TriggerOnClick (bool force = false)
    {
        _buttonOff = !_buttonOff;       // ! means retrieve the oppsite value 
        _menu.SetActive(_buttonOff);
    }
    
}
