using System.Net.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();

    }
    public void PlaySound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
