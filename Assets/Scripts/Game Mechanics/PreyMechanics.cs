using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyMechanics : MonoBehaviour
{
    public int health = 20;
    public int restoreAmount = 10;

    // How much damage to take from player
    public int damageAmount = 10;

    private GameObject player;

    private PlayerMechanics playerMechanics;
    private bool breachedSurface;
    int lifeStage;

    bool inRadius = false;
    bool attackPreviouslyActive = false;
    public string preyTag;

    // Start is called before the first frame update
    void Start()
    {
        preyTag = gameObject.tag;
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

        bool attackActive = playerMechanics.isAttacking();
        bool attackJustStarted = attackActive && !attackPreviouslyActive;
        attackPreviouslyActive = attackActive;

        if (inRadius && attackJustStarted)
        {
            {
                // When player attacks prey, decrease its health
                health -= damageAmount;

                // Update player's stats when enemy is defeated ("eaten")
                if (health <= 0)
                {
                    // Give player 1 evolution point
                    // Give special points for unique prey types

                    if (lifeStage == 4)
                    {
                        if (!playerMechanics.breachedSurface())
                        {
                            if (preyTag == "Aquatic")
                                playerMechanics.UpdateEvolutionProgress(1);
                        }
                        else
                        {
                            if (preyTag == "Terrestrial")
                                playerMechanics.UpdateEvolutionProgress(1);
                        }
                    }
                    // Reset evo meter when a different type of prey
                    // from the previous prey was consumed (for stage 5)
                    else if (lifeStage == 5)
                    {
                        if (preyTag == "SugarGlider")
                            playerMechanics.UpdateSugarGliderProgress(1);
                        else if (preyTag == "Jerboa")
                            playerMechanics.UpdateJerboaProgress(1);
                        else
                            playerMechanics.UpdateEvolutionProgress(1);
                    }
                    else
                        playerMechanics.UpdateEvolutionProgress(1);

                    // Replenish player health
                    playerMechanics.UpdateHealth(-1 * restoreAmount);
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
