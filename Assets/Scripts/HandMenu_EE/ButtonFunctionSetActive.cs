using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonFunctionSetActive : MonoBehaviour 
{
    // [SerializeField] private GameObject button;
    [SerializeField] private GameObject _menu;
    public bool _buttonOff = true;

    void Start()
    {
        _menu.SetActive(_buttonOff);
    }

    public void TriggerOnClick (bool force = false)
    {
        _buttonOff = !_buttonOff;

        _menu.SetActive(_buttonOff);
    }
}