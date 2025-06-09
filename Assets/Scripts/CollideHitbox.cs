using UnityEngine;

public class CollideHitbox : MonoBehaviour
{
    private GameObject player;
    public float damage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damage = GetComponentInParent<AIEnemyController>().damage;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered Attack");
        if (other.tag == "Player")
        {
            Debug.Log("Player take damage");
            player.GetComponent<PlayerCombat>().TakeDamage(damage);
        }
    }
}
