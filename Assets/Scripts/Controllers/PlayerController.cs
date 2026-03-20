using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private Animation anim;

    // Player's movement speed
    public float speed = 0;
    private float walkSpeed;
    private float sprintSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();

        // Store inputted speed
        walkSpeed = speed;
        sprintSpeed = walkSpeed * 1.5f;
    }

    // Called when move input detected
    void OnMove (InputValue movementValue)
    {
        // Convert input into vector
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Called once per fixed framerate frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Move the player
        rb.AddForce(movement * speed);
    }

    // Update is called once per frame
    void Update()
    {
        // Freeze player character when not moving
        /*
        if (Input.anyKey)
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
        */  

        // Walking handling (WASD)
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            anim.Play("walk");
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            anim.Stop();
        }

        // Sprinting handling (Shift)
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            speed = sprintSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed = walkSpeed;
        }

        // Attack handling (LMB)
        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("attack");
        }
    }
}
