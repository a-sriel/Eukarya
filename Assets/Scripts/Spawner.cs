using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject predatorPrefab;
    public GameObject preyPrefab;

    // Prefab groups
    GameObject predatorParent;
    GameObject preyParent;

    [Header("Spawn Settings")]
    public int predatorCount = 2;
    public int preyCount = 6;

    // Keep track of how many creatures left alive in game
    private int currentPredators = 0;
    private int currentPrey = 0;

    public Vector2 spawnAreaSize = new Vector2(20f, 20f);   // Vector indicating dimensions of spawn area
    public float ySpawnLevel = 14f;  // Height to spawn animals at

    // Cooldown timer
    private float predatorCooldown = 0f;
    private float preyCooldown = 0f;

    void Start()
    {
        predatorParent = new GameObject("Predators");
        preyParent = new GameObject("Prey");

        SpawnEntities(predatorParent, predatorPrefab, predatorCount, currentPredators);
        SpawnEntities(preyParent, preyPrefab, preyCount, currentPrey);
    }

    void Update()
    {
        // Fetch number of living creatures
        int currentPredators = predatorParent.transform.childCount;
        int currentPrey = preyParent.transform.childCount;

        // Use cooldown timers to ensure predators/prey don't respawn instantly upon deletion
        // ****** Predator timer
        // Tick
        if (predatorCooldown > 0)
        {
            predatorCooldown -= Time.deltaTime;

            // Respawn missing entities once timer runs out
            if (predatorCooldown <= 0)
            {
                SpawnEntities(predatorParent, predatorPrefab, predatorCount, currentPredators);
            }
        }

        // Initiate timer when entities not at max
        if (predatorCooldown <= 0 && currentPredators < predatorCount)
        {
            predatorCooldown = 3f;
        }
        // End predator timer

        // ****** Prey timer
        // Tick
        if (preyCooldown > 0)
        {
            preyCooldown -= Time.deltaTime;

            // Respawn missing entities once timer runs out
            if (preyCooldown <= 0)
            {
                SpawnEntities(preyParent, preyPrefab, preyCount, currentPrey);
            }
        }

        // Initiate timer when entities not at max
        if (preyCooldown <= 0 && currentPrey < preyCount)
        {
            preyCooldown = 3f;
        }
        // End prey timer
    }

    void SpawnEntities(GameObject prefabGroup, GameObject prefab, int count, int currentPrefabs)
    {
        for (int i = currentPrefabs; i < count; i++)
        {
            // Get random offset for spawn position
            float randomX = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
            float randomY = Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f);

            Vector3 spawnPosition = new Vector3(
                transform.position.x + randomX,
                ySpawnLevel,
                transform.position.z + randomY
            );

            // Add entity to game
            GameObject activePrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);

            // Parent newly spawned objects to their respective group
            activePrefab.transform.parent = prefabGroup.transform;
        }
    }

    // Visualizes spawn area when selecting Spawner in Inspector
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawCube(transform.position, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y));
    }
}
