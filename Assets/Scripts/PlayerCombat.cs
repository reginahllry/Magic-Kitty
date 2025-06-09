using UnityEngine;
using Cinemachine;

public class PlayerCombat : MonoBehaviour
{
    public float maxHealth = 150;
    float currentHealth;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    public float damage = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy Died!");
        }
    }

    public void AddMaxHealth()
    {
        maxHealth += 25;
        Heal();
    }

    public void Heal()
    {
        currentHealth = maxHealth;
    }
}
