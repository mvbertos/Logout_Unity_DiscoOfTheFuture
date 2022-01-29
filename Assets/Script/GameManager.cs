using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static PlayerMovement playerMovement;
    private static PlayerSwitchSide playerSwitchSide;
    [SerializeField] private KeyCode startGameInput = KeyCode.Space;
    public static KeyCode StartGameInput;


    private void Awake()
    {
        StartGameInput = startGameInput;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerSwitchSide = FindObjectOfType<PlayerSwitchSide>();

        DisablePLayerInputs();
    }

    private void Update()
    {
        if (Input.GetKeyDown(startGameInput))
            EnablePLayerInputs();
    }

    private static void DisablePLayerInputs()
    {
        bool value = false;
        playerMovement.enabled = value;
        playerSwitchSide.enabled = value;
    }
    private static void EnablePLayerInputs()
    {
        bool value = true;
        playerMovement.enabled = value;
        playerSwitchSide.enabled = value;
    }
}
