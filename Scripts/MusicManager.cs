using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip musica1;
    public AudioClip musica2;
    public float audioVolume;
    public bool fadeBool;
    public bool cambio;

    // Start is called before the first frame update
    void Start()
    {
        audioVolume = 1.0f;
        fadeBool = true;
        cambio = false;
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = musica1;
        audiosource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (cambio)
        {
            if (audiosource.clip == musica1)
            {
                if (audioVolume < 1 && !fadeBool)
                {
                    fadeIn();
                }
                else
                {
                    fadeOut();
                    if (audioVolume == 0)
                    {
                        audiosource.clip = musica2;
                        audiosource.Play();
                    }
                }
            }
            else
            {
                if (audioVolume < 1 && !fadeBool)
                {
                    fadeIn();
                }
                else
                {
                    fadeOut();
                    if (audioVolume == 0)
                    {
                        audiosource.clip = musica1;
                        audiosource.Play();
                    }
                }
            }
        }
    }

    void fadeIn()
    {
        if (audioVolume < 1)
        {
            audioVolume += Time.deltaTime;
            audiosource.volume = audioVolume;
        }
        if (audioVolume >= 1)
        {
            cambio = false;
            fadeBool = true;
            audioVolume = 1;
        }
    }

    void fadeOut()
    {
        if (audioVolume > 0)
        {
            audioVolume -= Time.deltaTime;
            audiosource.volume = audioVolume;

        }
        if (audioVolume <= 0)
        {
            audioVolume = 0;
            fadeBool = false;
        }
    }

    GameObject PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        tempGO.AddComponent<AudioSource>();
        tempGO.GetComponent<AudioSource>().clip = clip;
        tempGO.GetComponent<AudioSource>().Play();
        Destroy(tempGO, clip.length);
        return tempGO;
    }
}
