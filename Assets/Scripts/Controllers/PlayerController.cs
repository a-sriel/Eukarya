using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Player's movement speed
    public float speed = 0;
    private float walkSpeed;
    private float sprintSpeed = 0;

    // Stats for swimming scenes
    private Vector3 floating;
    public float floatForce = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Store inputted speed
        walkSpeed = speed;
        sprintSpeed = walkSpeed * 1.5f;

        floating = new Vector3(0.0f, floatForce, 0.0f);
    }

    // Called when move input detected
    void OnMove (InputValue movementValue)
    {
        // Convert input into vector
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public float rotationSpeed = 10f;
    public float facingOffset = 0f;
    public bool enableRotation = true;

    // Toggle for swimming scenes
    public bool enableFloating = false;

    // Called once per fixed framerate frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Transform movement to be relative to camera's horizontal facing
        if (Camera.main != null)
        {
            Quaternion camYaw = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f);
            movement = camYaw * movement;
        }

        // Move the player
        rb.AddForce(movement * speed);

        // Rotate to face movement direction (disabled for 2D scenes)
        if (enableRotation && movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement) * Quaternion.Euler(0f, facingOffset, 0f);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Sprinting handling (Shift)
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                speed = sprintSpeed;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed = walkSpeed;
        }

        // Floating handling for swimming (Spacebar)
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Physics.gravity, ForceMode.Acceleration);
            rb.AddForce(floating * 0.25f, ForceMode.Impulse);
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
                !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            // Sinking handling (Shift with no keypress)
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                rb.AddForce(Physics.gravity, ForceMode.Acceleration);
                rb.AddForce(floating * -10.25f, ForceMode.Force);
            }
        }
    }
}
