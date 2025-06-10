using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    [SerializeField] private float timer = 5;
    private float bulletTime;

    public GameObject enemyProjectile;
    public Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShootAtPlayer()
    {
           
    }
}
