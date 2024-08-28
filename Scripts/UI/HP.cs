using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [Header("HP")]
    
    [SerializeField] private TextMeshProUGUI HPValue;
    [SerializeField] private Animator restHpAnimator;

    [Header("Main Menu")]
    [SerializeField] private GameObject InfiniteHpOn;
    [SerializeField] private GameObject InfiniteHpOff;
    [SerializeField] private Button upgradeHpButton;


    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) //Main menu
        {
            StaticStats.hP = StaticStats.initialHp + StaticStats.extraHp;
            if (StaticStats.infiniteHp == true)
            {
                InfiniteHpOn.SetActive(true);
                InfiniteHpOff.SetActive(false);
            }
            else
            {
                InfiniteHpOn.SetActive(false);
                InfiniteHpOff.SetActive(true);
            }
            if(StaticStats.points <= StaticStats.pointsHpCost)
            {
                upgradeHpButton.interactable = false;
            }
        }

        else        
        {
            HPValue.text = "x" + StaticStats.hP.ToString();
        }
            
    }


    public void RestHP()
    {
        if (StaticStats.hP > 0)
        {
            StaticStats.hP -= 1;
            HPValue.text = "x" + StaticStats.hP.ToString();
            restHpAnimator.SetTrigger("Rest");
        }
        
        

    }

    
    public void UpHp()
    {
        if (StaticStats.points >= StaticStats.pointsHpCost)
        {
            StaticStats.extraHp += 1;
            StaticStats.hP = StaticStats.initialHp + StaticStats.extraHp;
            // HPValue.text = "x" + StaticStats.hP.ToString();
        }
        
    }

    public void ExtraHp()
    {

            StaticStats.hP += 3; 
            HPValue.text = "x" + StaticStats.hP.ToString();
        

    }

    public void InfiniteHp()
    {
        StaticStats.infiniteHp = true;
    }

    public void NormalHp()
    {
        StaticStats.infiniteHp = false;
    }


}
