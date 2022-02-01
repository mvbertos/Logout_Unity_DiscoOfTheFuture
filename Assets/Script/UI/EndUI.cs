using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUI : MonoBehaviour
{
    public void GoHome()
    {
        GameManager.ChangeScene("TittleScreen");
    }

    public void Relaod()
    {
        GameManager.ChangeScene(SceneManager.GetActiveScene().name);
    }
}
