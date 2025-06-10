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
        }

        if (spellType == 2)
        {
            anim.SetBool("water_cast", spellType == 2);
        }

        if (spellType == 3)
        {
            anim.SetBool("ice_cast", spellType == 3);
        }
        if (spellType == 4)
        {
            anim.SetBool("thunder_cast", spellType == 4);
        }

        float speed = 45f;

        // TODO: cant just do this bcs we didnt code it to look at the destination
        rb.linearVelocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 5 && other.gameObject.layer != 2)
        {
            rb.linearVelocity = Vector3.zero;
            if (other.tag == "Enemy")
            {
                // plays enemy impact sound in AIEnemyController
                enemy.GetComponent<AIEnemyController>().TakeDamage(damage);
            }

            else
            {
                SFXManager.Play("Hit", true);
            }

            anim.SetTrigger("death");
        }



    }

    public void Death()
    {
        Destroy(gameObject);
    }

}
