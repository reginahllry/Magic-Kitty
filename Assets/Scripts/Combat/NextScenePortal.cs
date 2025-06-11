using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScenePortal : MonoBehaviour
{
    public bool playerWin;
    public string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneName = gameObject.scene.name;
        playerWin = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if (playerWin) gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && playerWin)
        {
            // print("sceneNumber: " + sceneName[sceneName.Length - 1]);
            if (sceneName == "Level1")
            {
                SceneManager.LoadScene("Level2");
            }
            else if (sceneName == "Level2")
            {
                SceneManager.LoadScene("Level3");
            }
            else return;
        }
    }
}
