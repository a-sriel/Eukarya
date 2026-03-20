using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EvolutionManager : MonoBehaviour
{
    // Start life cycle at stage 1; will be updated as player evolves
    public int evolutionStage = 1;

    void Awake()
    {
        // Ensure evolution mechanic persists in every life stage
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void evolve()
    {
        evolutionStage++;
        switchScene();
    }

    public void switchScene()
    {
        switch (evolutionStage)
        {
            case 1:
                SceneManager.LoadScene("Stage2");
                break;
            case 2:
                SceneManager.LoadScene("Stage2");
                break;
        }
    }
}
