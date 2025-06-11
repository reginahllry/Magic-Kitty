using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePaused : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    public static bool GameisPaused = false;
    private string sceneName;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        sceneName = gameObject.scene.name;
    }

    void Update()
    {
        if (sceneName != gameObject.scene.name)
        {
            sceneName = gameObject.scene.name;
            return;   
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                if (sceneName == "Level3") return;

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
        sceneName = "MainMenu";
    }

    public void LoadOptionsMenu()
    {
        Debug.Log("Loading Options Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("");
    }
}
