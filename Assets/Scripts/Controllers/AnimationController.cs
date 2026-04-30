using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animation anim;
    private Rigidbody rb;
    private PlayerMechanics playerMechanics;

    private int stamina;

    public float moveThreshold = 0.1f;

    bool attacking = false;
    bool attackAnimationPlaying = false;

    // Freeze player movement if special animation is playing
    bool freezePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();

        playerMechanics = GetComponent<PlayerMechanics>();
    }

    // Update is called once per frame
    void Update()
    {
        stamina = playerMechanics.GetStamina();

        // Reset attack; only attack for one frame
        attacking = false;

        if (anim.IsPlaying("attack"))
            freezePlayer = true;
        else
            freezePlayer = false;

        // Walking handling (velocity-based)
        bool isMoving = rb.velocity.magnitude > moveThreshold;
        if (!anim.IsPlaying("attack"))
        {
            if (isMoving)
            {
                if (!anim.IsPlaying("walk"))
                    anim.Play("walk");
                anim["walk"].speed = 1f;
            }
            else
            {
                if (anim.IsPlaying("walk"))
                    anim["walk"].speed = 0f;
            }
        }
        
        // Check if enough stamina to attack
        if (stamina > 350)
        {
            // Attack handling (LMB); only attack while attack animation is not already playing
            // Prevents spamming attack and forces cooldown
            if (Input.GetMouseButtonDown(0) && !anim.IsPlaying("attack"))
            {
                anim.Play("attack");
                attacking = true;

                playerMechanics.UpdateStamina(350);
            }
        }

    }

    public bool isAttacking()
    {
        return attacking;
    }

    public bool specialAnimationPlaying()
    {
        return freezePlayer;
    }
}
