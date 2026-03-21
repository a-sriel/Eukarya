using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class StartMenuUIManager : MonoBehaviour
{
    private GameObject evolutionManagerObject;
    private EvolutionManager evolutionManager;

    public int evolutionStage;

    // Public function to load a scene by name
    public void NextScene()
    {
        SceneManager.LoadScene("Stage2");
    }

    // Start is called before the first frame update
    void Start()
    {
        evolutionManagerObject = GameObject.FindWithTag("EvolutionManager");
        evolutionManager = evolutionManagerObject.GetComponent<EvolutionManager>();

        evolutionStage = evolutionManager.GetEvolutionStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
