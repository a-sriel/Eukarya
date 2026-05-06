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
    bool attackPreviouslyActive = false;

    float cooldownPeriod = 3f;
    float attackCooldown = 0f;

    private bool breachedSurface;
    int lifeStage;

    public string predatorTag;

    // Start is called before the first frame update
    void Start()
    {
        predatorTag = gameObject.tag;
        TryFindPlayer();
    }

    void TryFindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) return;
        player = playerObj;
        playerMechanics = player.GetComponentInParent<PlayerMechanics>();
        if (playerMechanics == null) return;
        breachedSurface = playerMechanics.breachedSurface();
        lifeStage = playerMechanics.GetLifeStage();
    }


    // Detect if within player hitbox radius
    bool IsPlayerCollider(Collider other)
    {
        return other.gameObject.CompareTag("Player") || other.GetComponentInParent<PlayerMechanics>() != null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsPlayerCollider(other))
        {
            inRadius = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsPlayerCollider(other))
        {
            inRadius = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMechanics == null)
        {
            TryFindPlayer();
            if (playerMechanics == null) return;
        }

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

        bool attackActive = playerMechanics.isAttacking();
        bool attackJustStarted = attackActive && !attackPreviouslyActive;
        attackPreviouslyActive = attackActive;

        if (inRadius)
        {
            // Attack player, then initiate cooldown timer
            if (attackCooldown <= 0)
            {
                attackCooldown = cooldownPeriod;

                // When player collides with predator, decrease player's health
                playerMechanics.UpdateHealth(depleteAmount);
            }

            if (attackJustStarted)
            {
                // When player collides with predator, decrease its health
                health -= damageAmount;

                // Update player's stats when enemy is defeated
                if (health <= 0)
                {
                    // Check if in stage 4
                    if (lifeStage == 4)
                    {
                        if (!playerMechanics.breachedSurface())
                        {
                            if (predatorTag == "Aquatic")
                                playerMechanics.UpdateEvolutionProgress(1);
                        }
                        else
                        {
                            if (predatorTag == "Terrestrial")
                                playerMechanics.UpdateEvolutionProgress(1);
                        }
                    }
                    else
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
