using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void GoToLink(string _URL)
    {
        Application.OpenURL(_URL);
    }
}
