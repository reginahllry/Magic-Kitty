using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;
    private int spellType;
    private Rigidbody rb;
    private GameObject enemy;
    private float damage;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().damage;

        // 1 for fire
        // 2 for water
        // 3 for ice
        // 4 for thunder
        var rng = new System.Random();
        spellType = rng.Next(1, 5);

        Debug.Log("Spell Type: " + spellType);

        if (spellType == 1) anim.SetBool("fire_cast", true);
        if (spellType == 2) anim.SetBool("water_cast", true);
        if (spellType == 3) anim.SetBool("ice_cast", true);
        if (spellType == 4) anim.SetBool("thunder_cast", true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Projectile Start");
        float speed = 10f;

        // TODO: cant just do this bcs we didnt code it to look at the destination
        rb.linearVelocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy.GetComponent<AIEnemyController>().TakeDamage(damage);
        }

        anim.SetTrigger("death");
    }

    // void Die()
    // {
    //     anim.SetTrigger("death");
    //     Invoke(nameof(Death), 2f);
    // }

    public void Death()
    {
        Destroy(gameObject);
    }

}
