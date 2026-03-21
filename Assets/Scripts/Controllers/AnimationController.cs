using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animation anim;
    private Rigidbody rb;

    public float moveThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Walking handling (velocity-based)
        bool isMoving = rb.velocity.magnitude > moveThreshold;

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

        // Attack handling (LMB)
        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("attack");
        }
    }
}
