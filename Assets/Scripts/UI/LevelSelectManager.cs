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
        if (EvolutionManager.Instance != null)
        {
            int stage = SceneNameToStage(sceneName);
            EvolutionManager.Instance.SetStageDirect(stage);
        }
        SceneManager.LoadScene(sceneName);
    }

    int SceneNameToStage(string sceneName)
    {
        switch (sceneName)
        {
            case "Stage1": return 1;
            case "Stage2": return 2;
            case "Stage3": return 3;
            case "Stage4": return 4;
            case "Stage5": return 5;
            case "Stage6_Jump": return 6;
            case "Stage6_Fly": return 7;
            default: return 0;
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
