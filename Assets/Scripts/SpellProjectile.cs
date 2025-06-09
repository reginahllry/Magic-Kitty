using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask projectileLayerMask = new LayerMask();
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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 1 for fire
        // 2 for water
        // 3 for ice
        // 4 for thunder
        var rng = new System.Random();
        spellType = rng.Next(1, 5);

        Debug.Log("Spell Type: " + spellType);

        // anim.Rebind();
        // anim.Update(0f);

        if (spellType == 1)
        {
            anim.SetBool("fire_cast", spellType == 1);
            anim.Play("fire_cast", 0, 0f);
        }

        if (spellType == 2)
        {
            anim.SetBool("water_cast", spellType == 2);
            anim.Play("water_cast", 0, 0f);
        }

        if (spellType == 3)
        {
            anim.SetBool("ice_cast", spellType == 3);
            anim.Play("ice_cast", 0, 0f);
        }
        if (spellType == 4)
        {
            anim.SetBool("thunder_cast", spellType == 4);
            anim.Play("thunder_cast", 0, 0f);
        }

        float speed = 10f;

        // TODO: cant just do this bcs we didnt code it to look at the destination
        rb.linearVelocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        rb.linearVelocity = Vector3.zero;

        if (other.gameObject.layer != 5 | other.gameObject.layer != 2)
        {
            if (other.tag == "Enemy")
            {
                enemy.GetComponent<AIEnemyController>().TakeDamage(damage);
            }

            anim.SetTrigger("death");
        }

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
