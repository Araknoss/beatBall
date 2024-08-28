using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue=null;
    [SerializeField] private Slider volumeSlider=null;
    [SerializeField] private float defaultVolume=1.0f;

    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Graphics Settings")]
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private float defaultBrightness = 1.0f;
    private float brightnessLevel;

    [Header("Levels to load")]
    public string newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;
    [SerializeField] private GameObject loadGameDialog;

    public void SetBrightness(float brightness)
    {
        brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void ApplyBrightness()
    {
        PlayerPrefs.SetFloat("masterBrightness", brightnessLevel);
        StartCoroutine(ConfirmationBox());
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0"); //Darle valor en el panel
    }

    public void ApplyVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void DefaultButton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            ApplyVolume();
        }

        if(MenuType=="Graphics")
        {
            brightnessLevel = defaultBrightness;
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");
            ApplyBrightness();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }


    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGameDialogYes()
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
    }
}
