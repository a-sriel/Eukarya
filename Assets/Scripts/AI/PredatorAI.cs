using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PredatorAI : MonoBehaviour
{
    [Header("Chase Settings")]
    public float chaseSpeed = 7f;
    public float detectionDistance = 25f;

    [Header("Wander Settings")]
    public float wanderSpeed = 3f;
    public float wanderRadius = 40f;
    public float wanderWaitTime = 2.5f;

    private Transform player;
    private Rigidbody rb;
    private Vector2 wanderTarget;
    private float wanderTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        PickNewWanderTarget();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 myPos2D = new Vector2(rb.position.x, rb.position.z);
        Vector2 playerPos2D = new Vector2(player.position.x, player.position.z);

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
        Vector2 nextPos2D = Vector2.MoveTowards(myPos, targetPos, chaseSpeed * Time.fixedDeltaTime);

        rb.MovePosition(new Vector3(nextPos2D.x, rb.position.y, nextPos2D.y));
    }

    void WanderAround(Vector2 myPos)
    {
        wanderTimer -= Time.fixedDeltaTime;

        if (wanderTimer <= 0)
        {
            PickNewWanderTarget();
        }

        Vector2 nextPos2D = Vector2.MoveTowards(myPos, wanderTarget, wanderSpeed * Time.fixedDeltaTime);
        rb.MovePosition(new Vector3(nextPos2D.x, rb.position.y, nextPos2D.y));
    }

    void PickNewWanderTarget()
    {
        Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
        wanderTarget = new Vector2(rb.position.x, rb.position.z) + randomOffset;
        wanderTimer = wanderWaitTime;
    }
}