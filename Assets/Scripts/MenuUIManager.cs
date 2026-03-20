using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class MenuUIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool paused = false;
    private float timePaused;

    // Public function to load a scene by name
    public void NextScene()
    {
        SceneManager.LoadScene("Stage4");
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(paused);
        timePaused = paused ? 0f : 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            timePaused = paused ? 0f : 1f;
            pauseMenu.SetActive(paused);
            Time.timeScale = timePaused;
        }
    }
}
