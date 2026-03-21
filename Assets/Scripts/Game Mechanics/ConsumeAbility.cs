using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script should be attached to the player and all involved objects need the Health script as well
public class ConsumeAbility : MonoBehaviour
{
    // to detect potential consume targets
    public float consumeRange = 2f;
    public float healthGainOnConsume = 20f;

    void Update()
    {
        // Check for player input to initiate consume (e.g., "E" key or a button)
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryConsume();
        }
    }

    private void TryConsume()
    {
        // Use Physics.OverlapSphere (or BoxCast, etc. depending on your game type)
        // to find enemies within the consume range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, consumeRange);

        foreach (var hitCollider in hitColliders)
        {
            // Check if the detected object has the "Prey" tag and is dead
            if (hitCollider.CompareTag("Prey"))
            {
                Health enemyHealth = hitCollider.GetComponent<Health>();

                if (enemyHealth != null && enemyHealth.getisDead())
                {
                    // Perform the consumption
                    Consume(enemyHealth);
                    return; // Consume one enemy at a time
                }
            }
        }
    }

    private void Consume(Health consumedEnemyHealth)
    {

        // Remove the consumed enemy from the scene
        Destroy(consumedEnemyHealth.gameObject);
        Debug.Log("Enemy consumed! Gained " + healthGainOnConsume + " health.");

        // Add any other effects (visuals, sound, ability unlock, etc.)
    }
}
