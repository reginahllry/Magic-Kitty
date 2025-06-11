using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePaused : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    public static bool GameisPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resume Clicked");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu");
        Time.timeScale = 1f;
        PlayerPrefs.DeleteKey("StartMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadOptionsMenu()
    {
        Debug.Log("Loading Options Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("");
    }
}
