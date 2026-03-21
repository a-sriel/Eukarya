using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimals : MonoBehaviour
{
    public Vector2[] spawnPoints;

    public GameObject[] animals;
    private GameObject[] aliveAnimals;
    private int animalCount;

    public int maxAnimals = 30;

    public float leftBound = 0f;
    public float rightBound = 0f;

    public float forwardBound = 0f;
    public float backBound = 0f;

    public float yPosition = 0f;

    private float cooldown = 0f;
    private bool cooldownActive;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        // Restart cooldown timer
        if (cooldown <= 0)
        {
            if (animalCount > maxAnimals)
            {
                cooldown = 10f;
                spawn();
            }           
        }
    }

    void spawn()
    {
        int randomAnimal = Random.Range(0, animals.Length);
        Vector3 spawnPos = new Vector3(Random.Range(leftBound, rightBound), yPosition, Random.Range(backBound, forwardBound));
        Instantiate(animals[randomAnimal], spawnPos, animals[randomAnimal].transform.rotation);
    }
}
