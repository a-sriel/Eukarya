using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject predatorPrefab;
    public GameObject preyPrefab;

    [Header("Spawn Settings")]
    public int predatorCount = 2;
    public int preyCount = 6;
    public Vector2 spawnAreaSize = new Vector2(20f, 20f);   // Vector indicating dimensions of spawn area
    public float ySpawnLevel = 14f;  // Height to spawn animals at

    void Start()
    {
        SpawnEntities(predatorPrefab, predatorCount);
        SpawnEntities(preyPrefab, preyCount);
    }

    void SpawnEntities(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Get random offset for spawn position
            float randomX = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
            float randomY = Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f);

            Vector3 spawnPosition = new Vector3(
                transform.position.x + randomX,
                ySpawnLevel,
                transform.position.y + randomY
            );

            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }

    // Visualizes spawn area when selecting Spawner in Inspector
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawCube(transform.position, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y));
    }
}
