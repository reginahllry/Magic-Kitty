using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerCombat chloe;
    public GameObject gameOverPanel;
    public float delay = 1.5f;
    private bool gameOverTriggered = false;

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }
    void Update()
    {
        if (!gameOverTriggered && chloe != null && GetCurrentHealth() <= 0)
        {
            gameOverTriggered = true;
            StartCoroutine(ShowGameOverAfterDelay(1.5f)); // Adjust delay as needed
        }
    }

    float GetCurrentHealth()
    {
        var field = typeof(PlayerCombat).GetField("currentHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (float)field.GetValue(chloe);
    }

    IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 0f;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game");
        Time.timeScale = 1f;
                SceneManager.LoadScene("Level0");
    }

    public void ReturnToMain()
    {
        Debug.Log("Loading Main Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

