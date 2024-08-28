using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Next : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nextValue;
    [SerializeField] private Button nextButton;
    PruebaMovimiento pm;

    [Header("Main Menu")]
    [SerializeField] private Button upgradeNextButton;

    void Start()
    {
        if (StaticStats.next <=0 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            nextButton.interactable = false;
        }
        pm = GameObject.FindWithTag("Player").GetComponent<PruebaMovimiento>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StaticStats.next = StaticStats.initialNext + StaticStats.extraNext;
            if (StaticStats.points <= StaticStats.pointsNextCost)
            {
                upgradeNextButton.interactable = false;
            }
        }
        else
        {
            nextValue.text = "x" + StaticStats.next.ToString();
        }
                
    }
    
    public void PressNext()
    {
        if (StaticStats.next > 0)
        {
            StaticStats.next -= 1;
            nextValue.text = "x" + StaticStats.next.ToString();
            pm.NextLevel();
        }
        
    }

    public void UpNext()
    {
        if (StaticStats.points >= StaticStats.pointsNextCost)
        {
            StaticStats.extraNext += 1;
            StaticStats.next = StaticStats.initialNext + StaticStats.extraNext;
            //nextValue.text = "x" + StaticStats.next.ToString();
        }

    }
}
