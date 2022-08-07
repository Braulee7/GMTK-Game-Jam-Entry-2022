using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;

    public GameObject StartScreen;
    public GameObject pauseMenu;
    public GameObject HUD;

    private void Start()
    {
        Time.timeScale = 0f;
        isPaused = false;
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        StartScreen.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            } else 
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        HUD.SetActive(true);
        isPaused = false;
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        HUD.SetActive(false);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void Quit()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void Play()
    {
        Time.timeScale = 1f;
        HUD.SetActive(true);
        pauseMenu.SetActive(false);
        StartScreen.SetActive(false);
    }
}
