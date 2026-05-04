using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PreyAIFlying : MonoBehaviour
{
    [Header("Flee Settings")]
    public float fleeSpeed = 12f;
    public float detectionDistance = 30f;

    [Header("Wander Settings")]
    public float wanderSpeed = 4f;
    public float wanderRadius = 40f;
    public float wanderWaitTime = 2.5f;

    [Header("Movement Settings")]
    public float rotationSpeed = 10f;

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
        Vector2 nextPos2D = myPos + (directionAway2D * fleeSpeed * Time.fixedDeltaTime);

        rb.MovePosition(new Vector3(nextPos2D.x, rb.position.y, nextPos2D.y));

        FaceMovementDirection(new Vector3(directionAway2D.x, 0, directionAway2D.y));
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

        Vector2 moveDir2D = (wanderTarget - myPos).normalized;
        if (moveDir2D != Vector2.zero)
        {
            FaceMovementDirection(new Vector3(moveDir2D.x, 0, moveDir2D.y));
        }
    }

    void FaceMovementDirection(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion smoothRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        rb.MoveRotation(smoothRotation);
    }

    void PickNewWanderTarget()
    {
        Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
        wanderTarget = new Vector2(rb.position.x, rb.position.z) + randomOffset;
        wanderTimer = wanderWaitTime;
    }
}