using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool playerInRange = false;
    public GameObject portal;

        void Update()
        {
            print("bool player in range: " + playerInRange + "\nbool player pressing z: " + Input.GetKeyDown(KeyCode.Z));
            if (playerInRange && Input.GetKeyDown(KeyCode.Z))
            {
                TriggerDialogue();
            }
        }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        print("player exit?");
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void TriggerDialogue()
    {
        print("dialogue triggered");
        portal.SetActive(true);
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
