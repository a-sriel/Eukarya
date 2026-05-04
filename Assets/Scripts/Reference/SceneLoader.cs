using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Public function to load a scene by name
    public void NextScene()
    {
        SceneManager.LoadScene("Scene2");
    }

}
