using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreen;
    private bool paused = false;
    private float timePaused;

    public GameObject player;
    private PlayerMechanics playerMechanics;

    // Menu buttons management
    public void pause()
    {
        paused = true;
        timePaused = paused ? 0f : 1f;
        deathScreen.SetActive(paused);
        Time.timeScale = timePaused;

        // Lock/unlock cursor with pause state
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;

        
    }

    public void restart()
    {
        // Reload the scene from the beginning
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
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
        deathScreen.SetActive(false);

        playerMechanics = player.GetComponent<PlayerMechanics>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player is dead, stop the game
        if (playerMechanics.isDead())
        {
            pause();
        }
    }
}