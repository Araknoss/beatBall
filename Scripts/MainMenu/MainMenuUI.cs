using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Cementerio2");
    }

    /*public void Options()
    {
      SceneManager.LoadScene("Options");  
    }
    */

    public void QuitProgram()
    {
        Application.Quit();
    }
}
