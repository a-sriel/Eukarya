using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class StartMenuUIManager : MonoBehaviour
{
    // Public function to load a scene by name
    public void NextScene()
    {
        SceneManager.LoadScene("Stage4");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
