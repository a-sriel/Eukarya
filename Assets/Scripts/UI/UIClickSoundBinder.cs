using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIClickSoundBinder : MonoBehaviour
{
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        BindAll();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindAll();
    }

    void BindAll()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Button b in buttons)
        {
            b.onClick.RemoveListener(PlayClick);
            b.onClick.AddListener(PlayClick);
        }
    }

    void PlayClick()
    {
        if (SFXManager.Instance != null)
            SFXManager.Instance.PlayUIClick();
    }
}
