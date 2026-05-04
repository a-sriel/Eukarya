using UnityEngine;
using UnityEngine.SceneManagement;

public class StageUnlocker : MonoBehaviour
{
    void Awake()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt(sceneName + "_Unlocked", 1);

        // Stage6_Fly and Stage6_Jump unlock each other
        if (sceneName == "Stage6_Fly")
            PlayerPrefs.SetInt("Stage6_Jump_Unlocked", 1);
        else if (sceneName == "Stage6_Jump")
            PlayerPrefs.SetInt("Stage6_Fly_Unlocked", 1);

        PlayerPrefs.Save();
    }
}
