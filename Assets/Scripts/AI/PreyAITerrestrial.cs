using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PreyAITerrestrial : MonoBehaviour
{
    [Header("Flee Settings")]
    public float fleeSpeed = 12f;
    public float detectionDistance = 30f;
    public float fleeDistanceMultiplier = 1.5f;

    [Header("Wander Settings")]
    public float wanderSpeed = 4f;
    public float wanderRadius = 40f;
    public float wanderWaitTime = 2.5f;

    private NavMeshAgent agent;
    private Transform player;
    private float wanderTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        PickNewWanderTarget();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionDistance)
        {
            FleeFromPlayer();
        }
        else
        {
            WanderAround();
        }
    }

    void FleeFromPlayer()
    {
        agent.speed = fleeSpeed;

        Vector3 directionAway = (transform.position - player.position).normalized;
        Vector3 fleeDestination = transform.position + (directionAway * detectionDistance * fleeDistanceMultiplier);

        if (NavMesh.SamplePosition(fleeDestination, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void WanderAround()
    {
        agent.speed = wanderSpeed;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            wanderTimer -= Time.deltaTime;

            if (wanderTimer <= 0)
            {
                PickNewWanderTarget();
            }
        }
    }

    void PickNewWanderTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            wanderTimer = wanderWaitTime;
        }
    }
}