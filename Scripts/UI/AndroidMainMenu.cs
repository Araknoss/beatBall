using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AndroidMainMenu : MonoBehaviour
{

    //Levels
    [Header("Levels to load")]
    public string newGameLevel;
    private string levelToLoad;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        //audioSource = Camera.main.GetComponent<AudioSource>();
    }
    public void AudioOn()
    {
        audioSource.mute = false;
    }

    public void AudioOff()
    {
        audioSource.mute = true;
    }

    public void NewGameDialogYes()
    {
        Invoke("LoadLevel", 1);
        
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(newGameLevel);
    }

   /* public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }

        else
        {
            noSavedGameDialog.SetActive(true);
            loadGameDialog.SetActive(false);
        }
    }

    public void QuitProgram()
    {
        Application.Quit();
    }*/
}
