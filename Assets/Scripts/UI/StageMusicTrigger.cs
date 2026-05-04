using UnityEngine;

public class StageMusicTrigger : MonoBehaviour
{
    public string trackName;

    void Start()
    {
        if (MusicManager.Instance == null) return;

        if (string.IsNullOrEmpty(trackName) || trackName == "none")
            MusicManager.Instance.StopMusic();
        else
            MusicManager.Instance.PlayTrack(trackName);
    }
}
