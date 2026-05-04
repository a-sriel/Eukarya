using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioClip cellMusic;
    public AudioClip underwaterMusic;
    public AudioClip forestMusic;

    [Range(0f, 1f)] public float cellVolume = 1f;
    [Range(0f, 1f)] public float underwaterVolume = 1f;
    [Range(0f, 1f)] public float forestVolume = 1f;

    private float masterVolume = 1f;
    private AudioSource audioSource;
    private string currentTrack = "";

    public void SetMasterVolume(float v)
    {
        masterVolume = Mathf.Clamp01(v);
        audioSource.volume = GetTrackBaseVolume() * masterVolume;
    }

    float GetTrackBaseVolume()
    {
        if (currentTrack == "cell") return cellVolume;
        if (currentTrack == "underwater") return underwaterVolume;
        if (currentTrack == "forest") return forestVolume;
        return 1f;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void PlayTrack(string trackName)
    {
        if (currentTrack == trackName) return;

        currentTrack = trackName;
        AudioClip clip = null;
        float volume = 1f;
        if (trackName == "cell") { clip = cellMusic; volume = cellVolume; }
        else if (trackName == "underwater") { clip = underwaterMusic; volume = underwaterVolume; }
        else if (trackName == "forest") { clip = forestMusic; volume = forestVolume; }

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.volume = volume * masterVolume;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        currentTrack = "";
        audioSource.Stop();
    }
}
