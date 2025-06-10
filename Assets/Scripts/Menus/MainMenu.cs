using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;

    void Start()
    {
        string startIn = PlayerPrefs.GetString("StartIn", "MainMenu");

        if (startIn == "Options")
        {
            mainMenuPanel.SetActive(false);
            optionsMenuPanel.SetActive(true);
        }
        else
        {
            mainMenuPanel.SetActive(true);
            optionsMenuPanel.SetActive(false);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void QuitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void OpenOptionsMenu()
    {
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    public void BacktoMenu()
    {
        optionsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

}
