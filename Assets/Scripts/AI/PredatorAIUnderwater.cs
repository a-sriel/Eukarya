using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorAIUnderwater : MonoBehaviour
{
    public enum FacingAxis { Forward, Up, Right }

    [Header("Movement Options")]
    public bool useRotation = true;
    public FacingAxis orientationAxis = FacingAxis.Forward;
    public float rotationSpeed = 5f;

    [Header("Chase Settings")]
    public float chaseSpeed = 7f;
    public float detectionDistance = 25f;

    [Header("Burst Settings")]
    public bool enableBursts = true;
    public float burstMultiplier = 2.5f;
    public float burstDuration = 0.5f;
    public float burstCooldown = 2.0f;

    [Header("Wander Settings")]
    public float wanderSpeed = 3f;
    public float wanderRadius = 40f;
    public float wanderWaitTime = 2.5f;

    private Transform player;
    private Vector3 wanderTarget;
    private float wanderTimer;
    private float burstTimer;
    private bool isBursting;

    void Start()
    {
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
            HandleBurstLogic();
            float currentSpeed = isBursting ? chaseSpeed * burstMultiplier : chaseSpeed;
            MoveAndRotate(player.position, currentSpeed);
        }
        else
        {
            WanderLogic();
        }
    }

    void HandleBurstLogic()
    {
        if (!enableBursts) return;

        burstTimer -= Time.deltaTime;
        if (burstTimer <= 0)
        {
            isBursting = !isBursting;
            burstTimer = isBursting ? burstDuration : burstCooldown;
        }
    }

    void MoveAndRotate(Vector3 targetPos, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (useRotation)
        {
            Vector3 direction = (targetPos - transform.position).normalized;
            if (direction != Vector3.zero) ApplySmoothRotation(direction);
        }
    }

    void WanderLogic()
    {
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0) PickNewWanderTarget();

        MoveAndRotate(wanderTarget, wanderSpeed);
    }

    void ApplySmoothRotation(Vector3 direction)
    {
        Quaternion targetRotation;
        if (orientationAxis == FacingAxis.Up)
            targetRotation = Quaternion.LookRotation(Vector3.Cross(direction, transform.right), direction);
        else if (orientationAxis == FacingAxis.Right)
            targetRotation = Quaternion.LookRotation(Vector3.Cross(transform.up, direction), Vector3.up);
        else
            targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void PickNewWanderTarget()
    {
        Vector3 randomOffset = Random.insideUnitSphere * wanderRadius;
        wanderTarget = transform.position + randomOffset;
        wanderTimer = wanderWaitTime;
    }
}