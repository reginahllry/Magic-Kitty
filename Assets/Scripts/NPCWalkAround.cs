using UnityEngine;
using UnityEngine.AI;

public class NPCWalkAround : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    public Transform player;

    // Base Variable Values
    float waitTime = 4f;
    public float speedWalk = 5f;
    public float timeToRotate = 2f;
    Vector3 playerLastPosition = Vector3.zero;

    // Agent Variables
    private float m_waitTime;
    private float m_TimeToRotate;
    private bool m_PlayerNear;
    int m_currentWaypointIndex;
    Vector3 m_playerPosition;

    public Transform[] waypoints;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        m_currentWaypointIndex = 0;
        m_TimeToRotate = timeToRotate;
        agent.speed = speedWalk;
        agent.SetDestination(waypoints[m_currentWaypointIndex].position);
    }

    void Update()
    {
        Patrolling();
    }

    private void NextPoint()
    {
        m_currentWaypointIndex = (m_currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[m_currentWaypointIndex].position);
    }

    void Patrolling()
    {
        agent.SetDestination(waypoints[m_currentWaypointIndex].position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (m_waitTime <= 0)
            {
                NextPoint();
                Move(speedWalk);
                m_waitTime = waitTime;
            }

            else
            {
                m_waitTime -= Time.deltaTime;
            }
        }
    }

    void Move(float speed)
    {
        agent.isStopped = false;
        agent.speed = speed;
    }
}
