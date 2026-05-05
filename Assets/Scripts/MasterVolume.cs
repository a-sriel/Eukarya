using UnityEngine;

public class MasterVolume : MonoBehaviour
{
    [Range(0f, 5f)] public float masterVolume = 3f;

    void Update()
    {
        AudioListener.volume = masterVolume;
    }

    void OnValidate()
    {
        AudioListener.volume = masterVolume;
    }
}
