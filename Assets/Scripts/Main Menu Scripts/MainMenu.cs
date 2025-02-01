using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;

    void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void onPlayClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void onOptionsClick()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void onQuitClick()
    {
        Application.Quit();
    }

    public void onExitClick()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
