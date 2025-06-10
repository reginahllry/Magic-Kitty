using UnityEngine;

public class CollideHitbox : MonoBehaviour
{
    private PlayerCombat player;
    private AIEnemyController enemy;
    private float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damage = enemy.damage;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered Attack");
        if (other.tag == "Player")
        {
            Debug.Log("Player take damage");
            player.TakeDamage(damage);
        }
    }
}
