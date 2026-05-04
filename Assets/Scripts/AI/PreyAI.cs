using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyAI : MonoBehaviour
{
    [Header("Flee Settings")]
    public float fleeSpeed = 12f;
    public float detectionDistance = 30f;

    [Header("Wander Settings")]
    public float wanderSpeed = 4f;
    public float wanderRadius = 40f;
    public float wanderWaitTime = 2.5f;

    private Transform player;
    private Vector2 wanderTarget;
    private float wanderTimer;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        PickNewWanderTarget();
    }

    void Update()
    {
        if (player == null) return;

        Vector2 myPos2D = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPos2D = new Vector2(player.position.x, player.position.z);

        float distanceToPlayer = Vector2.Distance(myPos2D, playerPos2D);

        if (distanceToPlayer <= detectionDistance)
        {
            FleeFromPlayer(myPos2D, playerPos2D);
        }
        else
        {
            WanderAround(myPos2D);
        }
    }

    void FleeFromPlayer(Vector2 myPos, Vector2 playerPos)
    {
        Vector2 directionAway2D = (myPos - playerPos).normalized;

        Vector3 moveVector = new Vector3(directionAway2D.x, 0f, directionAway2D.y);

        transform.Translate(moveVector * fleeSpeed * Time.deltaTime, Space.World);
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
        Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
        wanderTarget = new Vector2(transform.position.x, transform.position.z) + randomOffset;
        wanderTimer = wanderWaitTime;
    }
}
