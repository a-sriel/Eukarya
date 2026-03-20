using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorMechanics : MonoBehaviour
{
    public int health = 20;

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

        // When player collides with prey, decrease player's health
        if (other.gameObject.CompareTag("Player"))
        {
            playerMechanics = player.GetComponent<PlayerMechanics>();
            playerMechanics.UpdateHealth(2);
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
