using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour {

    public string mainMenuLevel;
    public string playGameLevel;

    public void RestartGame()
    {
        
        FindObjectOfType<GameManager>().Reset();
        Application.LoadLevel(playGameLevel);
    }
    public void QuitToMain()
    {
        Application.LoadLevel(mainMenuLevel);
        
        
    }
}
