using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EvolutionManager : MonoBehaviour
{
    // Start life cycle at stage 1; will be updated as player evolves
    public int evolutionStage = 0;
    private int oldEvolutionStage = 0;

    public static EvolutionManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        // Ensure evolution mechanic persists in every life stage
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        oldEvolutionStage = evolutionStage;
    }

    // Update is called once per frame
    void Update()
    {
        if (evolutionStage != oldEvolutionStage)
        {
            switchScene();
            oldEvolutionStage = evolutionStage;
        }
    }

    public void evolve()
    {
        evolutionStage++;
    }

    // Sets stage without triggering switchScene (used by Level Select)
    public void SetStageDirect(int stage)
    {
        evolutionStage = stage;
        oldEvolutionStage = stage;
    }

    public void evolveToJerboa()
    {
        evolutionStage = 6;
    }

    public void evolveToSugarGlider()
    {
        evolutionStage = 7;
    }

    public void winGame()
    {
        evolutionStage = 8;
    }

    public void switchScene()
    {
        switch (evolutionStage)
        {
            case 0:
                SceneManager.LoadScene("TitleScreen");
                break;
            case 1:
                SceneManager.LoadScene("Stage1");
                break;
            case 2:
                SceneManager.LoadScene("Stage2");
                break;
            case 3:
                SceneManager.LoadScene("Stage3");
                break;
            case 4:
                SceneManager.LoadScene("Stage4");
                break;
            case 5:
                SceneManager.LoadScene("Stage5");
                break;
            case 6:
                SceneManager.LoadScene("Stage6_Jump");
                break;
            case 7:
                SceneManager.LoadScene("Stage6_Fly");
                break;
            case 8:
                SceneManager.LoadScene("WinScreen");
                break;
        }
    }

    // ******Getter
    public int GetEvolutionStage()
    {
        return evolutionStage;
    }
}
