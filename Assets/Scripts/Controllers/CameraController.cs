using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    // Orbit settings
    public float distance = 10f;
    public float mouseSensitivity = 3f;
    public float pitchMin = -20f;
    public float pitchMax = 60f;

    private float yaw = 0f;
    private float pitch = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Called once per frame, after all Update functions finished
    void LateUpdate()
    {
        if (Time.timeScale == 0f) return;

        // Accumulate mouse input into orbit angles
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        // Position camera by rotating offset around player
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = player.transform.position + rotation * new Vector3(0f, 0f, -distance);
        transform.LookAt(player.transform.position);
    }
}
