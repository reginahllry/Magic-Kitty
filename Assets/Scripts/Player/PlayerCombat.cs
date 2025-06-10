using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class PlayerCombat : MonoBehaviour
{
    public float maxHealth = 150;
    float currentHealth;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private Animator anim;
    public float damage = 20f;

    // public ParticleSystem blast;
    // public List<ParticleCollisionEvent> collisionEvents;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim.enabled = true;
        currentHealth = maxHealth;
        anim = gameObject.GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        anim.SetTrigger("Hurt");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            anim.SetTrigger("Death");
        }
    }

    public void Die()
    {
        SFXManager.Play("ChloeDeath", false, 1f);
        Invoke(nameof(GameOver), 1.5f);
        // Debug.Log("time scale: "+ Time.timeScale);
    }

    void GameOver()
    {
        Time.timeScale = 0;
        anim.enabled = false;
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

    // void OnParticleCollision(GameObject other)
    // {
    //     Debug.Log("BLAST HIT PLAYER");
    // }
}
