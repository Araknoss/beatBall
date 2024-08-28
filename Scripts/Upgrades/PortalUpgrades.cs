using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PortalUpgrades : MonoBehaviour
{
    [SerializeField] private Vector3 position1;
    [SerializeField] private Vector3 position2;
    //[SerializeField] private Vector3 position3;
    public float timer;
    [SerializeField] private GameObject hP;
    [SerializeField] private GameObject next;
   
    

    void Update()
    {
        
      // timer -=Time.deltaTime;
        if (timer < 0)
        {
            timer = 2;
        }
        if (timer<1f && timer>=0)
        {
            SetPosition(1);
        }
        else if (timer<2f && timer>=1f)
        {
            SetPosition(2);
        }
      /*  else if (timer<3f && timer>=2f)
        {
            SetPosition(3);
        }*/
    }

   

    void SetPosition(int currentPosition)
    {        
        
        switch (currentPosition)
        {
            case 1:
                transform.position = position1;
                hP.SetActive(true);
                next.SetActive(false);
                break;
            case 2:
                transform.position = position2;
                next.SetActive(true);
                hP.SetActive(false);
                break;
           /* case 3:
                transform.position = position3;
                break;*/
        }
    }


}
