using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Toggle seagullsToggle;
    [SerializeField] AudioSource seagulls;

    private AudioSource[] audioSources;

    void Start()
    {
        // load audio sources
        audioSources = gameObject.GetComponentsInChildren<AudioSource>();
        
        // if there is no MasterVolume set it to 0.2 
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            PlayerPrefs.SetFloat("MasterVolume", 0.2f);
        }
        setVolume();
    }

    private void setVolume()
    {
        foreach (AudioSource source in audioSources)
        {
            float newVolume = PlayerPrefs.GetFloat("MasterVolume");
            if (source.name == "Seagulls")
            {
                newVolume *= 0.05f;
            }

            source.volume = newVolume;
        }
    }

    public void onSliderUpdate()
    {
        PlayerPrefs.SetFloat("MasterVolume", slider.value);
        setVolume();
    }

    private void toggleSeagulls()
    {
        int currentValue = PlayerPrefs.GetInt("MuteSeagulls");
        
        // can you check with just if(currentValue)?? the else is just catching when current value is 0
        if (currentValue == 1)
        {
            seagulls.mute = true;
        }
        else
        {
            seagulls.mute = false;
        }
    }

    public void onToggleSeagulls()
    {
        // its inverted
        PlayerPrefs.SetInt("MuteSeagulls", seagullsToggle.isOn ? 0 : 1);
        toggleSeagulls();
    }

    public void playLossMusic()
    {
        GameObject.Find("Music").GetComponent<AudioSource>().Stop();
        GameObject.Find("LossMusic").GetComponent<AudioSource>().Play();
    }

    public void playWinMusic()
    {
        GameObject.Find("Music").GetComponent<AudioSource>().Stop();
        GameObject.Find("WinMusic").GetComponent<AudioSource>().Play();
    }
}
