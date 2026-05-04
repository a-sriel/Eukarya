using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    public GameObject winScreen;
    private bool shown = false;

    public void Show()
    {
        if (shown) return;
        shown = true;
        winScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (MusicManager.Instance != null)
            MusicManager.Instance.StopMusic();
    }

    public void goToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }

    public void exit()
    {
        Application.Quit();
    }

    void Start()
    {
        winScreen.SetActive(false);
    }
}
