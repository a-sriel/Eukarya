using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private AnimationController animationController;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Player's movement speed
    private float speed;
    public float walkSpeed = 15;
    private float sprintSpeed = 0;

    // Stats for swimming scenes
    private Vector3 floating;
    public float floatForce = 2.0f;

    bool stopMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Store inputted speed
        speed = walkSpeed;
        sprintSpeed = walkSpeed * 2.5f;

        floating = new Vector3(0.0f, floatForce, 0.0f);
        rb.useGravity = true;

        animationController = gameObject.GetComponent<AnimationController>();
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
        stopMoving = animationController.specialAnimationPlaying();

        if (stopMoving)
            speed = 0;
        else
        {
            // Sprinting handling (Shift)
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                    Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    print("SPRINTING");
                    speed = sprintSpeed;
                }
            }

            // if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            else
            {
                speed = walkSpeed;
            }
        }

        if (enableFloating)
        {
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

    public void enableSwimming(bool flag)
    {
        enableFloating = flag;
    }
}
