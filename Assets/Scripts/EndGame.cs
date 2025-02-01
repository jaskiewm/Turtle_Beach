using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public UIManager uiManager;
    public AudioManager audioManager;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (uiManager.isGameOver)
        {
            return;
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (gameObject.tag == "Seagull" && collision.gameObject.tag == "Player")
        {
            uiManager.displayEndMenu(false);
            audioManager.playLossMusic();
        }
        else if (gameObject.tag == "Endzone" && collision.gameObject.tag == "Player")
        {
            uiManager.displayEndMenu(true);
            audioManager.playWinMusic();
        }
    }
}
