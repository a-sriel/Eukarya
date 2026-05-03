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

    public OptionsManager optionsManager;
    public GameObject levelSelectPanel;
    public GameObject dimOverlay;

    public void OpenOptions()
    {
        optionsManager.Open();
    }

    public void OpenLevelSelect()
    {
        dimOverlay.SetActive(true);
        levelSelectPanel.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        dimOverlay.SetActive(false);
        levelSelectPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void QuitGame()
    {
        Application.Quit();
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
