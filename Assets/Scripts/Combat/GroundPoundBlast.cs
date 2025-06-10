using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundPoundBlast : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject enemy;
    public ParticleSystem blast;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        damage = enemy.GetComponent<AIEnemyController>().damage;
        blast = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        print("ParticleCollision:" + other.gameObject.tag);
        int numCollisionEvents = blast.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb && rb.gameObject.tag == "Player")
            {
                print("particlecollision");
                rb.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
            }

            i++;
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Blast hits something: " + other.gameObject.layer);

    //     if (other.gameObject.layer != 6 && other.gameObject.layer != 9 && other.gameObject.layer != 10)
    //     {
    //         if (other.tag == "Player")
    //         {
    //             Debug.Log("Blast Hits Player");
    //             other.GetComponent<PlayerCombat>().TakeDamage(damage);
    //         }
    //     }
    // }
}

