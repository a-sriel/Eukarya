using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorMechanics : MonoBehaviour
{
    public int health = 20;
    public int depleteAmount = 2;

    // How much damage to take from player
    public int damageAmount = 10;

    private GameObject player;

    private PlayerMechanics playerMechanics;

    bool inRadius = false;

    float cooldownPeriod = 3f;
    float attackCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj;
    }


    // Detect if within player hitbox radius
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRadius = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRadius = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ****** Attack timer
        // Tick
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;

            // Respawn missing entities once timer runs out
            if (attackCooldown <= 0)
            {
            }
        }

        if (inRadius)
        {
            playerMechanics = player.GetComponentInParent<PlayerMechanics>();

            // Attack player, then initiate cooldown timer
            if (attackCooldown <= 0)
            {
                attackCooldown = cooldownPeriod;

                // When player collides with predator, decrease player's health
                playerMechanics.UpdateHealth(depleteAmount);
            }

            // Check if attack animation is playing
            if (playerMechanics.isAttacking())
            {
                // When player collides with predator, decrease its health
                health -= damageAmount;

                // Update player's stats when enemy is defeated
                if (health <= 0)
                {
                    playerMechanics.UpdateEvolutionProgress(1);
                }
            }
        }
        // When health is depleted, disappear from game
        if (health == 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
