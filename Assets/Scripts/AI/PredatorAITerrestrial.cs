using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PredatorAITerrestrial : MonoBehaviour
{
    [Header("Chase Settings")]
    public float chaseSpeed = 7f;
    public float detectionDistance = 25f;
    public float chaseUpdateInterval = 0.1f; // How often to recalculate the player's position

    [Header("Wander Settings")]
    public float wanderSpeed = 3f;
    public float wanderRadius = 40f;
    public float wanderWaitTime = 2.5f;

    private NavMeshAgent agent;
    private Transform player;
    private float wanderTimer;
    private float chaseTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        PickNewWanderTarget();
    }

    void Update()
    {
        if (player == null || !agent.isOnNavMesh) return;

        // DEBUG: Visualize the target destination
        if (agent.hasPath)
        {
            Debug.DrawLine(transform.position, agent.destination, Color.green);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionDistance)
        {
            ChasePlayer();
        }
        else
        {
            WanderAround();
        }
    }

    void ChasePlayer()
    {
        agent.speed = chaseSpeed;

        chaseTimer -= Time.deltaTime;
        if (chaseTimer <= 0)
        {
            agent.SetDestination(player.position);
            chaseTimer = chaseUpdateInterval;
        }
    }

    void WanderAround()
    {
        agent.speed = wanderSpeed;

        // Check if we have arrived at the wander target
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
        // Calculate a random point on the XZ plane
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        // Snap that point to the closest valid NavMesh position
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            wanderTimer = wanderWaitTime;
        }
    }
}