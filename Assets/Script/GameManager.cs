using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static PlayerMovement playerMovement;
    private static PlayerSwitchSide playerSwitchSide;
    [SerializeField] private Canvas endUIRef;
    public static Canvas EndUIRef;
    public static bool finished;

    [Header("Input")]
    [SerializeField] private KeyCode startGameInput = KeyCode.Space;
    public static KeyCode StartGameInput;

    [Header("Sounds")]
    [SerializeField] private MusicController musicController;
    public static MusicController Music;


    private void Awake()
    {
        StartGameInput = startGameInput;
        EndUIRef = endUIRef;
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
        if (!finished)
            EndGame();


    }

    private void EndGame()
    {
        if (Input.GetKeyDown(startGameInput) && !playerMovement.enabled && !playerSwitchSide.enabled)
        {
            EnablePLayerInputs();
            musicController.PlaySound();
        }

        //If don´t find player
        if (!playerMovement && !playerSwitchSide)
        {
            //Stop music
            musicController.audioSource.Stop();
            //reload scene 
            Invoke("ReloadScene", 2f);
        }
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public static void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public static void FinishGame()
    {
        Instantiate<Canvas>(EndUIRef);
        finished = true;
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
