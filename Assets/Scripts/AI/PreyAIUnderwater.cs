using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PreyAIUnderwater : MonoBehaviour
{
    public enum FacingAxis { Forward, Up, Right }

    [Header("Physics & Rotation")]
    public bool useRotation = true;
    public FacingAxis orientationAxis = FacingAxis.Forward;
    public float rotationSpeed = 4f;
    public float drag = 3f;

    [Header("Flee Settings")]
    public float fleeSpeed = 12f;
    public float detectionDistance = 30f;
    [Range(0f, 1f)] public float fleeDirectionVariance = 0.5f;

    [Header("Burst Settings")]
    public bool enableBursts = true;
    public float burstMultiplier = 3.0f;
    public float burstDuration = 0.4f;
    public float burstCooldown = 1.2f;

    [Header("Wander Settings")]
    public float wanderSpeed = 4f;
    public float wanderRadius = 40f;
    public float wanderWaitTime = 2.5f;

    private Rigidbody rb;
    private Transform player;
    private Vector3 wanderTarget;
    private Vector3 currentFleeDirection;
    private float wanderTimer;
    private float burstTimer;
    private bool isBursting;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
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
            HandleFleeLogic();
        }
        else
        {
            WanderLogic();
        }
    }

    void HandleFleeLogic()
    {
        if (enableBursts)
        {
            burstTimer -= Time.deltaTime;
            if (burstTimer <= 0)
            {
                isBursting = !isBursting;
                burstTimer = isBursting ? burstDuration : burstCooldown;
                if (isBursting) currentFleeDirection = CalculateErraticDirection();
            }
        }
        else
        {
            currentFleeDirection = (transform.position - player.position).normalized;
        }

        float speed = isBursting ? fleeSpeed * burstMultiplier : fleeSpeed;
        MoveAndRotate(transform.position + currentFleeDirection, speed);
    }

    Vector3 CalculateErraticDirection()
    {
        Vector3 directAway = (transform.position - player.position).normalized;
        return Vector3.Lerp(directAway, Random.insideUnitSphere, fleeDirectionVariance).normalized;
    }

    void MoveAndRotate(Vector3 targetPos, float speed)
    {
        Vector3 direction = (targetPos - transform.position).normalized;

        rb.velocity = direction * speed;

        if (useRotation && direction != Vector3.zero)
        {
            Quaternion targetRotation = (orientationAxis == FacingAxis.Up)
                ? Quaternion.LookRotation(Vector3.Cross(direction, transform.right), direction)
                : Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
    }

    void WanderLogic()
    {
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0) PickNewWanderTarget();
        MoveAndRotate(wanderTarget, wanderSpeed);
    }

    void PickNewWanderTarget()
    {
        Vector3 randomOffset = Random.insideUnitSphere * wanderRadius;
        wanderTarget = transform.position + randomOffset;
        wanderTimer = wanderWaitTime;
    }
}