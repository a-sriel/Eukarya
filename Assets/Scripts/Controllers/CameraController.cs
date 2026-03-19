using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    // Distance between camera and player
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called once per frame, after all Update functions finished
    void LateUpdate()
    {
        // Maintain same offset
        transform.position = player.transform.position + offset;
    }
}
