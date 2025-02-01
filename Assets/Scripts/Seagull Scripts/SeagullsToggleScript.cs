using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeagullsToggleScript : MonoBehaviour
{
    public GameObject seagulls;

    private void Start()
    {
        setVolume();
    }

    public void toggleSeagulls()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("MutedSeagulls", 1);
        }
        else
        {
            PlayerPrefs.SetInt("MutedSeagulls", 0);
        }

        setVolume();
    }

    private void setVolume()
    {
        if (PlayerPrefs.GetInt("MutedSeagulls") == 1)
        {
            seagulls.SetActive(false);
        }
        else
        {
            seagulls.SetActive(true);
        }
    }
}
