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

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj;
    }

    void OnTriggerEnter(Collider other)
    {
        // When player collides with prey, decrease its health
        if (other.gameObject.CompareTag("Player"))
        {
            health -= damageAmount;

            // Update player's stats when enemy is defeated ("eaten")
            if (health <= 0)
            {
                playerMechanics = other.GetComponentInParent<PlayerMechanics>();

                // Gain 1 evolution point
                playerMechanics.UpdateEvolutionProgress(1);

                // Replenish health
                playerMechanics.UpdateHealth(-1*restoreAmount);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        // When health is depleted, disappear from game
        if (health == 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
