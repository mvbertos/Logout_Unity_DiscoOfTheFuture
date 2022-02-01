using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public void KillCharacter()
    {
        GameManager.Lose();
        GameObject.Destroy(this.gameObject);
    }
}
