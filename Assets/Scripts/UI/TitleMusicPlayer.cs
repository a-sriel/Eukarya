using System.Collections;
using UnityEngine;

public class TitleMusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public float restSeconds = 5f;

    void Start()
    {
        StartCoroutine(LoopWithRest());
    }

    IEnumerator LoopWithRest()
    {
        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            yield return new WaitForSeconds(restSeconds);
        }
    }
}
