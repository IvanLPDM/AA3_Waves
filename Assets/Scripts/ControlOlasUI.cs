using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOlasUI : MonoBehaviour
{
    public GameObject objetoSinusoidal;  
    public GameObject objetoGerstner;    

    public Toggle toggleSinusoidal;
    public Toggle toggleGerstner;

    void Start()
    {
        toggleSinusoidal.onValueChanged.AddListener(OnToggleSinusoidalChanged);
        toggleGerstner.onValueChanged.AddListener(OnToggleGerstnerChanged);

        objetoSinusoidal.SetActive(toggleSinusoidal.isOn);
        objetoGerstner.SetActive(toggleGerstner.isOn);
    }

    void OnToggleSinusoidalChanged(bool isOn)
    {
        objetoSinusoidal.SetActive(isOn);
    }

    void OnToggleGerstnerChanged(bool isOn)
    {
        objetoGerstner.SetActive(isOn);
    }
}