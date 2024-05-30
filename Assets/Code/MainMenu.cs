using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameEZ()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void PlayGameMED()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void PlayGameHARD()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void Back2MM()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}