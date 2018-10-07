using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public string mainMenuLevel;
    public string playGameLevel;
    public GameObject pauseMenu;
    public Scroll scrollBackground;
    public PlayerController thePlayer;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        scrollBackground.scrollSpeed = 1;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        scrollBackground.scrollSpeed = scrollBackground.scrollSpeedStore;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        FindObjectOfType<GameManager>().Reset();
        Application.LoadLevel(playGameLevel);
    }
    public void QuitToMain()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(mainMenuLevel);
    }
}
