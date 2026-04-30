using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    public int health = 50;
    public int stamina = 50;
    public int evolutionProgress = 0;

    private int maxStamina;

    public bool readyToEvolve = false;

    private float cooldown = 0f;
    private bool cooldownActive;

    // Start life cycle at stage 1; will be updated as player evolves
    public int evolutionStage;

    private GameObject evolutionManagerObject;
    private EvolutionManager evolutionManager;
    private AnimationController animationController;
    private PlayerController playerController;

    bool dead = false;
    bool attacking = false;
    bool freezeInPlace = false;

    // Start is called before the first frame update
    void Start()
    {
        maxStamina = stamina;
        print(maxStamina);

        animationController = gameObject.GetComponent<AnimationController>();
        playerController = gameObject.GetComponent<PlayerController>();

        evolutionManagerObject = GameObject.FindWithTag("EvolutionManager");
        evolutionManager = evolutionManagerObject.GetComponent<EvolutionManager>();

        evolutionStage = evolutionManager.GetEvolutionStage();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Replenish stamina
        if (stamina < maxStamina)
        {
            stamina++;
        }

        attacking = animationController.isAttacking();

        // Each evolution progress point has a cooldown timer
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;

            // If timer runs out and no progress made, decrease progress
            if (cooldown <= 0)
            {
                evolutionProgress -= 1;
            }
        }

        // Restart cooldown timer
        if (evolutionProgress >= 1 && cooldown <= 0)
        {
            cooldown = 25f;
        }

        if (evolutionProgress < 5)
        {
            readyToEvolve = false;
        }

        // Toggle evolution function
        if (evolutionProgress == 5)
        {
            readyToEvolve = true;

            if(Input.GetKeyDown(KeyCode.E))
            {
                evolutionManager.evolve();
            }
        }

        if (health <= 0)
        {
            dead = true;
        }
    }

    public bool isDead()
    {
        return dead;
    }

    // ******Getters for player stats
    public int GetHealth()
    {
        return health;
    }

    public int GetStamina()
    {
        return stamina;
    }

    public int GetEvolutionProgress()
    {
        return evolutionProgress;
    }
    // End getters for player stats

    // ******Setters for player stats
    public void UpdateHealth(int damageAmount)
    {
        health -= damageAmount;
    }

    public void UpdateStamina(int energyAmount)
    {
        stamina -= energyAmount;
    }

    public void UpdateEvolutionProgress(int progressAmount)
    {
        evolutionProgress += progressAmount;
    }

    public int GetLifeStage()
    {
        return evolutionStage;
    }

    public bool isAttacking()
    {
        return attacking;
    }

    // End setters for player stats
}

