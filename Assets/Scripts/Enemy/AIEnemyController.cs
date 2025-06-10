using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class AIEnemyController : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public GameObject player;
    public Animator animator;
    public GameObject plane;
    public SpriteRenderer sprite;


    [Header("Utilities")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public float walkSpeed = 5.5f;
    public float waitTimer = 4f;
    public float agentWaitingTimer = 0f;
    public LayerMask whatIsGround, whatIsPlayer;


    [Header("Combat")]
    public float health;
    public float damage = 7f;
    public float timeBetweenAttacks;
    bool alreadyAttacked;


    [Header("Patroling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRangeX, walkPointRangeZ;
    private bool isFacingLeft;

    private bool playingFootsteps;
    private string sceneName;

    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponent<SpriteRenderer>();

        isFacingLeft = false;
        walkPointSet = false;

        agent.speed = walkSpeed;

        Renderer planeRenderer = plane.GetComponent<Renderer>();
        Vector3 planeSize = planeRenderer.bounds.size;

        walkPointRangeX = planeSize.x / 2;
        walkPointRangeZ = planeSize.z / 2;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        CheckFlip();
    }

    private void Patroling()
    {   
        if (sceneName == "Level1" && !playingFootsteps)
        {
            float speed = 0.5f;
            StartFootsteps(speed);
        }

        else if (sceneName == "Level2" && !playingFootsteps)
        {
            float speed = 0.4f;
            StartFootsteps(speed);
        }
        
        if (agentWaitingTimer >= 0)
        {
            agentWaitingTimer -= Time.deltaTime;
            Debug.Log("Waiting Timer: " + agentWaitingTimer);
        }

        else
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
            {
                Debug.Log("Walking");
                animator.SetBool("Walk", true);
                agent.SetDestination(walkPoint);
            }

            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            Debug.Log("Distance Left: " + distanceToWalkPoint.magnitude);

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
            {
                Debug.Log("Reached Destination");
                walkPointSet = false;
                animator.SetBool("Walk", false);
                agentWaitingTimer = waitTimer;

                if (sceneName == "Level1" && playingFootsteps)
                {
                    StopFootsteps();
                }

                else if (sceneName == "Level2" && playingFootsteps)
                {
                    StopFootsteps();
                    StartFootsteps(0.5f);
                }
            }
        }
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range

        Debug.Log("Searching walkpoint");
        float randomZ = Random.Range(-walkPointRangeZ, walkPointRangeZ);
        float randomX = Random.Range(-walkPointRangeX, walkPointRangeX);
        // float randomX = Random.Range(-walkpointRange, walkpointRange);
        // float randomZ = Random.Range(-walkpointRange, walkpointRange);

        walkPoint = new Vector3(randomX, transform.position.y, randomZ);

        walkPointSet = true;

        Debug.Log(walkPoint);
    }

    private void ChasePlayer()
    {
        
        if (sceneName == "Level1" && !playingFootsteps)
        {
            float speed = 0.5f;
            StartFootsteps(speed);
        }

        else if (sceneName == "Level2" && !playingFootsteps)
        {
            float speed = 0.3f;
            StartFootsteps(speed);
        }

        walkPointSet = false;
        Debug.Log("Chasing player");
        agent.SetDestination(player.transform.position);
        animator.SetBool("Walk", true);
    }

    private void AttackPlayer()
    {
        walkPointSet = false;
        //Make sure enemy doesn't move
        animator.SetBool("Walk", false);
        agent.SetDestination(transform.position);

        // if havent attacked
        if (!alreadyAttacked)
        {
            animator.SetTrigger("Attack");

            Vector3 halfBoxSize = new Vector3(8f, 2.5f, 5f);
            Vector3 offset = new Vector3(5f, 0f, 0f);

            if (isFacingLeft)
            {
                if (Physics.CheckBox(transform.position - offset, halfBoxSize))
                {
                    Invoke(nameof(DamagePlayer), 0.5f);
                }
            }

            else
            {
                if (Physics.CheckBox(transform.position + offset, halfBoxSize))
                {
                    Invoke(nameof(DamagePlayer), 0.5f);
                }
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void DamagePlayer()
    {
        player.GetComponent<PlayerCombat>().TakeDamage(damage);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        if (sceneName == "Level1")
        {
            SFXManager.Play("Impact_BOD");
        }

        else if (sceneName == "Level2")
        {
            SFXManager.Play("Impact_FlyingDemon");
        }

        health -= damage;

        if (health <= 0)
        {
            agent.SetDestination(transform.position);
            animator.SetTrigger("Death");
            StopFootsteps();
            Invoke(nameof(DestroyEnemy), 1f);
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Vector3 halfBoxSize = new Vector3(8f, 2.5f, 5f);
        Vector3 offset = new Vector3(5f, 0f, 0f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + offset, halfBoxSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position - offset, halfBoxSize);
    }

    private void CheckFlip()
    {
        float xPatrolDir = (transform.position - agent.destination).x;
        float xPlayerDir = (transform.position - player.transform.position).x;

        if (playerInAttackRange)
        {
            if (xPlayerDir > 0) isFacingLeft = true;
            else isFacingLeft = false;

            if (isFacingLeft) sprite.flipX = false;
            else sprite.flipX = true;

            Debug.Log("FacingLeft: " + isFacingLeft + "\nPlayerDir: " + xPlayerDir);
        }

        else
        {
            // positive xPatrolDir means it's going left
            if (xPatrolDir > 0) isFacingLeft = true;
            else isFacingLeft = false;

            if (isFacingLeft) sprite.flipX = false;
            else sprite.flipX = true;

            Debug.Log("FacingLeft: " + isFacingLeft + "\nPatrolDir: " + xPatrolDir);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            animator.SetTrigger("Hurt");
            agent.SetDestination(transform.position);
        }
    }


    void PlayFootsteps()
    {
        Debug.Log("Playing ENEMY footsteps");

        if (sceneName == "Level1")
        {
            SFXManager.Play("Footsteps_BOD", true);
        }

        else if (sceneName == "Level2")
        {
            SFXManager.Play("Wings", true);
        }
    }
    
    void StartFootsteps(float footstepsSpeed)
    {
        Debug.Log("Start ENEMY FOOTSTEPS with Speed: "+footstepsSpeed);
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootsteps), 0f, footstepsSpeed);
    }

    void StopFootsteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootsteps));
    }
}
