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

    bool evolveToJerboa = false;
    bool evolveToSugarGlider = false;

    int jerboaProgress = 0;
    int sugarGliderProgress = 0;

    bool tiktaalik = false;

    bool aquaticPhase = false;
    bool terrestrialPhase = false;

    // Start is called before the first frame update
    void Start()
    {
        maxStamina = stamina;
        print(maxStamina);

        animationController = gameObject.GetComponent<AnimationController>();
        playerController = gameObject.GetComponent<PlayerController>();

        evolutionManagerObject = GameObject.FindWithTag("EvolutionManager");
        if (evolutionManagerObject == null)
        {
            Debug.LogError("EvolutionManager not found! Make sure it has the 'EvolutionManager' tag.");
            return;
        }
        evolutionManager = evolutionManagerObject.GetComponent<EvolutionManager>();

        evolutionStage = evolutionManager.GetEvolutionStage();

        // Determine if in tiktaalik stage (this stage has 2 parts: terrestrial & aquatic
        tiktaalik = (evolutionStage == 4);
        if (tiktaalik)
        {
            aquaticPhase = true;
        }
        else terrestrialPhase = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (evolutionStage == 4)
        {
            if (terrestrialPhase)
            {
                //GameObject hitBox = gameObject.transform.GetChild(2).gameObject;
                //hitBox.layer = LayerMask.NameToLayer("BreachedSurface");
                gameObject.layer = LayerMask.NameToLayer("BreachedSurface");
                foreach (Transform child in transform) child.gameObject.layer = LayerMask.NameToLayer("BreachedSurface");
                print("BREACHED SURFACE");
            }
        }
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
        if (evolutionProgress == 5 && evolutionStage <= 5)
        {
            readyToEvolve = true;

            if(Input.GetKeyDown(KeyCode.E))
            {
                if (evolutionStage == 4 && aquaticPhase) // Only fully evolve upon terrestrial phase
                {
                    print("AQUATIC");
                    // Reset evo progress
                    aquaticPhase = false;
                    terrestrialPhase = true;
                    evolutionProgress = 0;

                    if (MusicManager.Instance != null)
                        MusicManager.Instance.PlayTrack("forest");

                    // Enable land movement
                }
                else if (evolveToJerboa)
                    evolutionManager.evolveToJerboa();
                else if (evolveToSugarGlider)
                    evolutionManager.evolveToSugarGlider();
                else
                    evolutionManager.evolve();
            }
        }

        if (evolutionProgress == 5 && evolutionStage > 5)
        {
            readyToEvolve = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                evolutionManager.winGame();
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

    public void UpdateJerboaProgress(int progressAmount)
    {
        evolveToJerboa = true;
        evolveToSugarGlider = false;

        sugarGliderProgress = 0;
        jerboaProgress += progressAmount;

        evolutionProgress = jerboaProgress;
    }

    public void UpdateSugarGliderProgress(int progressAmount)
    {
        evolveToSugarGlider = true;
        evolveToJerboa = false;

        jerboaProgress = 0;
        sugarGliderProgress += progressAmount;

        evolutionProgress = sugarGliderProgress;
    }

    public bool breachedSurface()
    {
        return terrestrialPhase;
    }

    // End setters for player stats
}

