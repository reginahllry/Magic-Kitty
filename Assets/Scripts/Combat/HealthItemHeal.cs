using System;
using UnityEngine;

public class HealthItemHeal : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private int minHealthHeal = 5;
    private int maxHealthHeal = 20;
    private float m_heal;
    private float timerUntilDestroy = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        System.Random rng = new System.Random();
        m_heal = rng.Next(minHealthHeal, maxHealthHeal);

        Invoke(nameof(Despawn), timerUntilDestroy);
    }

    void Despawn()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SFXManager.Play("Heal", true);
            player.GetComponent<PlayerCombat>().Heal(m_heal);
            Despawn();
        }
    }
}
