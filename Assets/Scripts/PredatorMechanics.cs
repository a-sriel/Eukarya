using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorMechanics : MonoBehaviour
{
    public int health = 20;
    public int depleteAmount = 2;

    public GameObject player;

    // How much damage to take from player
    public int damageAmount = 10;

    private PlayerMechanics playerMechanics;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        print("Predator touched!");
        print(other.gameObject.tag);


        if (other.gameObject.CompareTag("Player"))
        {
            // When player collides with predator, decrease its health
            health -= damageAmount;

            // When player collides with predator, decrease player's health
            playerMechanics = player.GetComponent<PlayerMechanics>();
            playerMechanics.UpdateHealth(depleteAmount);

            // Update player's stats when enemy is defeated
            if (health <= 0)
            {
                playerMechanics.UpdateEvolutionProgress(1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When health is depleted, disappear from game
        if (health == 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
