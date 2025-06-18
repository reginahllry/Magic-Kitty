using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Animator popUpAnimator;
    public string animationTrigger = "pop upp"; // nama trigger animasi kamu di Animator
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            popUpAnimator.SetTrigger(animationTrigger);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
