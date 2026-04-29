using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterMechanics : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;

    bool isUnderwater = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponentInParent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isUnderwater = true;
            playerController.enableSwimming(isUnderwater);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isUnderwater = false;
            playerController.enableSwimming(isUnderwater);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
