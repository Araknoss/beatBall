using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField] private AudioClip[] audios;
   [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(int index, float volume)
    {
        audioSource.PlayOneShot(audios[index], volume);
    }
    

    public void PauseMainTheme()
    {
        AudioSource mainAudioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        mainAudioSource.mute = true;

    }
    public void PlayMainTheme()
    {
        AudioSource mainAudioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        mainAudioSource.mute = false;
    }

}
