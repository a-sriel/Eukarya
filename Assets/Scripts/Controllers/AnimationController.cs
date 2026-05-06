using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (Time.timeScale == 0f) return;

        stamina = playerMechanics.GetStamina();

        // Attack flag follows the animation so prey have multiple frames to detect it
        attacking = anim.IsPlaying("attack");

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

        if (playerMechanics.GetLifeStage() == 7)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (SFXManager.Instance != null)
                    SFXManager.Instance.PlaySugarGliderFlap();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                anim.Play("fly");
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                anim.Stop();
            }
        }
        
        // Check if enough stamina to attack
        if (stamina > 350)
        {
            // Attack handling (LMB); only attack while attack animation is not already playing
            // Prevents spamming attack and forces cooldown
            if (Input.GetMouseButtonDown(0) && !anim.IsPlaying("attack") && !EventSystem.current.IsPointerOverGameObject())
            {
                anim.Play("attack");
                attacking = true;

                playerMechanics.UpdateStamina(350);

                if (SFXManager.Instance != null)
                    SFXManager.Instance.PlayAttack();
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
