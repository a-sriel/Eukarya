using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EvolutionManager : MonoBehaviour
{
    // Start life cycle at stage 1; will be updated as player evolves
    public int evolutionStage = 0;
    private int oldEvolutionStage = 0;

    void Awake()
    {
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
        }
    }

    // ******Getter
    public int GetEvolutionStage()
    {
        return evolutionStage;
    }
}
