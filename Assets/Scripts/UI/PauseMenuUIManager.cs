using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class PauseMenuUIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject dimOverlay;
    public OptionsManager optionsManager;
    private bool paused = false;
    private float timePaused;

    // Menu buttons management
    public void pause()
    {
        paused = !paused;
        timePaused = paused ? 0f : 1f;
        pauseMenu.SetActive(paused);
        if (dimOverlay != null) dimOverlay.SetActive(paused);
        Time.timeScale = timePaused;

        // Lock/unlock cursor with pause state
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;
    }

    public void OpenOptions()
    {
        optionsManager.Open();
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void exit()
    {
        Application.Quit();
    }
    // End menu buttons management

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        timePaused = paused ? 0f : 1f;
        Time.timeScale = timePaused;
        pauseMenu.SetActive(false);
        if (dimOverlay != null) dimOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
    }
}