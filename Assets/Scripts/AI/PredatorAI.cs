using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorAI : MonoBehaviour
{
    [Header("Chase Settings")]
    public float chaseSpeed = 7f;   // Speed to chase target
    public float detectionDistance = 25f;   // Distance to detect targets

    [Header("Wander Settings")]
    public float wanderSpeed = 3f;  // Speed to wander when no targets
    public float wanderRadius = 40f;    // Radius in which to pick a spot to wander to
    public float wanderWaitTime = 2.5f; // Time to wait between selecting targets to wander to

    private Transform player;
    private Vector2 wanderTarget;
    private float wanderTimer;

    void Start()
    {
        // Find player object
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        // Pick first target to wander to
        PickNewWanderTarget();
    }

    void Update()
    {
        if (player == null) return;

        // Get player's position and predator's position as 2D vectors
        Vector2 myPos2D = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPos2D = new Vector2(player.position.x, player.position.z);

        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(myPos2D, playerPos2D);

        if (distanceToPlayer <= detectionDistance)
        {
            ChasePlayer(myPos2D, playerPos2D);
        }
        else
        {
            WanderAround(myPos2D);
        }
    }

    void ChasePlayer(Vector2 myPos, Vector2 targetPos)
    {
        Vector2 newPos2D = Vector2.MoveTowards(myPos, targetPos, chaseSpeed * Time.deltaTime);
        transform.position = new Vector3(newPos2D.x, transform.position.y, newPos2D.y);
    }

    void WanderAround(Vector2 myPos)
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            PickNewWanderTarget();
        }

        Vector2 newPos2D = Vector2.MoveTowards(myPos, wanderTarget, wanderSpeed * Time.deltaTime);
        transform.position = new Vector3(newPos2D.x, transform.position.y, newPos2D.y);
    }

    void PickNewWanderTarget()
    {
        // Pick a random target within wanderRadius of the predator
        Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;

        wanderTarget = new Vector2(transform.position.x, transform.position.z) + randomOffset;

        wanderTimer = wanderWaitTime;
    }
}
