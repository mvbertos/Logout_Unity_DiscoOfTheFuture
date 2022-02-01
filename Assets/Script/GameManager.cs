using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Player
    private static PlayerMovement playerMovement;
    private static PlayerSwitchSide playerSwitchSide;

    //UI
    [SerializeField] private Canvas endUIRef;
    public static Canvas EndUIRef;

    //Game
    public static bool finished;
    public static GameManager instance;

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
        Music = musicController;
        instance = this;
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
        EnableCharacterControll();
    }

    private void PlayMusic()
    {
        musicController.PlaySound();
    }

    private void EnableCharacterControll()
    {
        if (playerMovement && playerSwitchSide)
        {
            if (Input.GetKeyDown(startGameInput) && !playerMovement.enabled && !playerSwitchSide.enabled)
            {
                playerMovement.enabled = true;
                playerSwitchSide.enabled = true;
                PlayMusic();
            }
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public static void Lose()
    {
        //Stop music
        Music.audioSource.Stop();
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
