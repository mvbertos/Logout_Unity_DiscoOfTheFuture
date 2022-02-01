using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    public void GoHome()
    {
        GameManager.ChangeScene("TittleScreen");
    }

    public void Relaod()
    {
        GameManager.ReloadScene();
    }
}
