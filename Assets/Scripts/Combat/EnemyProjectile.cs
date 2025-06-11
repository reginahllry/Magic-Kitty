using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject enemy;
    private int spellType;
    private Rigidbody rb;
    private int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        damage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AIEnemyController>().damage;
    }

    void Start()
    {
        anim.SetBool("fire_cast", true);
        float speed = 60f;
        rb.linearVelocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9 && other.gameObject.layer != 2 && other.gameObject.layer != 10)
        {
            SFXManager.Play("FD_Hit", true, 0.7f);

            rb.linearVelocity = Vector3.zero;

            if (other.tag == "Player")
            {
                // plays enemy impact sound in AIEnemyController
                player.GetComponent<PlayerCombat>().TakeDamage(damage);
            }

            anim.SetTrigger("death");
        }
    }
    
    public void Death()
    {
        Destroy(gameObject);
    }
}
