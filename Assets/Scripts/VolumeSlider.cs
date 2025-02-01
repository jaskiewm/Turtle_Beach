using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            slider.SetValueWithoutNotify(PlayerPrefs.GetFloat("MasterVolume"));
        }
        else
        {
            slider.SetValueWithoutNotify(0.2f);
        }
    }
}
