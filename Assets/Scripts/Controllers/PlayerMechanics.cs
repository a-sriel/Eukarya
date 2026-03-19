using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerMechanics : MonoBehaviour
{
    public int health = 50;
    public int stamina = 50;
    public int evolutionProgress = 0;

    public bool readyToEvolve = false;

    private float cooldown = 0f;
    private bool cooldownActive;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Predator"))
        {
            print("Enter Pred");
        }

        if (other.gameObject.CompareTag("Prey"))
        {

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Predator"))
        {
            print("Exit Pred");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Each evolution progress point has a cooldown timer
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            print(cooldown);

            // If timer runs out and no progress made, decrease progress
            if (cooldown <= 0)
            {
                evolutionProgress -= 1;
            }
        }

        if (evolutionProgress >= 1 && cooldown <= 0)
        {
            cooldown = 5f;
        }

        if (evolutionProgress == 5)
        {
            readyToEvolve = true;
        }
    }

    // Setters for player stats
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

    public void evolve()
    {
        // SceneManager.LoadScene("Scene2");
    }

}
