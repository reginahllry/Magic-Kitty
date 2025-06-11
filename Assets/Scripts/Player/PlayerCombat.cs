using UnityEngine;
using Cinemachine;
using System;

public class PlayerCombat : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerHealed;
    // public static event Action OnPlayerDeath;

    public float maxHealth = 200;
    public float currentHealth;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private Animator anim;
    public int damage = 20;
    public GameOverManager gom;

    // public ParticleSystem blast;
    // public List<ParticleCollisionEvent> collisionEvents;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim.enabled = true;
        currentHealth = maxHealth;
        anim = gameObject.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnPlayerDamaged?.Invoke();

        anim.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            anim.SetTrigger("Death");
        }
    }

    public void Die()
    {
        if (gameObject.scene.name == "Level 3")
        {
            SFXManager.Play("Gunshot");
            return;
        }

        SFXManager.Play("ChloeDeath", false, 1f);
        Invoke(nameof(GameOver), 0f);
        // Debug.Log("time scale: "+ Time.timeScale);
    }

    void GameOver()
    {
        anim.enabled = false;

        gom.GameOver();
        print("PLayerCombat GameOver triggered");
    }

    public void AddMaxHealth()
    {
        maxHealth += 25;
        currentHealth = maxHealth;
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        OnPlayerHealed?.Invoke();

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // void OnParticleCollision(GameObject other)
    // {
    //     Debug.Log("BLAST HIT PLAYER");
    // }
}
