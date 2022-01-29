using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressButtonText : MonoBehaviour
{
    [SerializeField] private TMP_Text TextRef;
    // Start is called before the first frame update
    void Start()
    {
        TextRef.text = $"Press\n{GameManager.StartGameInput}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameManager.StartGameInput))
        {
            Destroy(this.gameObject);
        }
    }
}
