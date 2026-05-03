using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    [System.Serializable]
    public class StageButton
    {
        public string sceneName;
        public Button button;
        public GameObject lockOverlay;
    }

    public StageButton[] stages;

    void Start()
    {
        foreach (StageButton stage in stages)
        {
            bool unlocked = IsUnlocked(stage.sceneName);
            stage.button.interactable = unlocked;
            if (stage.lockOverlay != null)
                stage.lockOverlay.SetActive(!unlocked);
        }
    }

    bool IsUnlocked(string sceneName)
    {
        if (sceneName == "Stage1") return true;
        // Stage6_Fly and Stage6_Jump share the same unlock key
        if (sceneName == "Stage6_Jump")
            return PlayerPrefs.GetInt("Stage6_Fly_Unlocked", 0) == 1;
        return PlayerPrefs.GetInt(sceneName + "_Unlocked", 0) == 1;
    }

    public void LoadStage(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoBack()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
