using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject chloe;
    public GameObject gameOverPanel;
    public float delay = 1.5f;
    private bool gameOverTriggered = false;
    private string sceneName;

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        sceneName = gameObject.scene.name;
    }
    void Update()
    {
        // if (GetCurrentHealth() <= 0)
        // {
        //     gameOverTriggered = true;
        //     StartCoroutine(ShowGameOverAfterDelay(1.5f)); // Adjust delay as needed
        // }

        if (sceneName != gameObject.scene.name)
        {
            sceneName = gameObject.scene.name;
        }
    }

    public void GameOver()
    {
        gameOverTriggered = true;
        StartCoroutine(ShowGameOverAfterDelay(1.5f));
    }

    float GetCurrentHealth()
    {
        float health = chloe.GetComponent<PlayerCombat>().currentHealth;
        return health;
    }

    IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Time.timeScale = 0f;

        bool isNull = gameOverPanel == null;

        print("is gameoverPanel null: " + isNull);
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game");
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnToMain()
    {
        Debug.Log("Loading Main Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

