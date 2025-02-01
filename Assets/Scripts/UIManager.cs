using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject endMenu;
    [SerializeField] GameObject GUIMenu;
    [SerializeField] TMP_Text endMenuTitle;
    [SerializeField] Slider cooldownSlider;
    [SerializeField] PlayerController player;

    private bool isPaused;
    public bool isGameOver;

    // Runs once at startup
    void Start()
    {
        // Reset timescale
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        endMenu.SetActive(false);
        GUIMenu.SetActive(true);
        isPaused = false;
    }

    // Runs once every frame.
    void Update()
    {
        if (Input.GetButtonDown("Pause") && !isPaused)
        {
            pause();
        }
        else if (Input.GetButtonDown("Pause") && isPaused)
        {
            resume();
        }
    }

    // Pause Menu
    private void pause()
    {
        pauseMenu.SetActive(true);
        GUIMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void resume()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        GUIMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void onResumeClick()
    {
        resume();
    }

    public void onOptionsClick()
    {
        pauseMenu.SetActive(false);
        GUIMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void onMainMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    // Options Menu
    public void onExitClick()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        GUIMenu.SetActive(false);
    }

    // End Menu

    public void displayEndMenu(bool isGameWon)
    {
        string endText = "";

        if (isGameOver)
        {
            return;
        }

        if (isGameWon)
        {
            endText = "You Win!";
        }
        else
        {
            endText = "You Lose";
        }

        isGameOver = true;
        GUIMenu.SetActive(false);
        endMenuTitle.SetText(endText);
        endMenu.SetActive(true);
        Time.timeScale = 0.2f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void onPlayAgainClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onQuitClick()
    {
        Application.Quit();
    }

    // Cooldown Slider
    public void startSliderCooldown()
    {
        StartCoroutine(sliderCooldown());
    }

    private IEnumerator sliderCooldown()
    {
       while (!player.canDash)
        {
            cooldownSlider.value -= Time.deltaTime;
            yield return null;
        }
        cooldownSlider.value = 5.5f;
    }
}
