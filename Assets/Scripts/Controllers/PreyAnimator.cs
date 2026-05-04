using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyAnimator : MonoBehaviour
{
    private Animation anim;

    // Freeze player movement if special animation is playing
    bool freezePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play("walk");
    }
}
