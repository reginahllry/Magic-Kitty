using UnityEngine;
using UnityEngine.AI;

public class npcPatrol1 : MonoBehaviour
{
    public Transform[] waypoints;            // Titik-titik yang akan dilalui NPC
    private int currentWaypointIndex = 0;    // Indeks waypoint saat ini
    private NavMeshAgent agent;              // Komponen NavMeshAgent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Pastikan ada waypoint yang diisi sebelum mengatur tujuan
        if (waypoints != null && waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        // Jangan lakukan apa-apa jika waypoint tidak ada
        if (waypoints == null || waypoints.Length == 0)
            return;

        // Periksa jika agen sudah hampir sampai di waypoint
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}
