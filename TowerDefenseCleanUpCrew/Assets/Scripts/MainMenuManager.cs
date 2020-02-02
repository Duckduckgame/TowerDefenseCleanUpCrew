using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void StartLevelOne()
    {
        SceneManager.LoadScene(1);
    }

    public void StartLevelTwo()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Tutorial()
    {

    }
}
