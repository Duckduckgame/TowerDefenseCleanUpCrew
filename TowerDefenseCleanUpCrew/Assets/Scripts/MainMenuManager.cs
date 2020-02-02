using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject menuCanvas;

    [SerializeField]
    GameObject tutorialCanvas;

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
        menuCanvas.SetActive(false);
        tutorialCanvas.SetActive(true);
    }

    public void BackToMenu()
    {
        menuCanvas.SetActive(true);
        tutorialCanvas.SetActive(false);
    }
}
