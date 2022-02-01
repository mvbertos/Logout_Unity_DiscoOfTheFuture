using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Canvas LoseUI;
    [SerializeField] private Canvas WinUI;
    
    public void KillCharacter()
    {
        GameManager.Lose();
        Instantiate(LoseUI);
        GameObject.Destroy(this.gameObject);
    }
}
