using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reescalar : MonoBehaviour
{
    [SerializeField] private List<GameObject> objetosReescalados  = new List<GameObject>();
    private GameObject rightWall;
    private GameObject leftWall;
    
    private void Start()
    {
        
        rightWall = transform.GetChild(1).gameObject;
        leftWall = transform.GetChild(2).gameObject;
        AjustarPosicionParedes();
        //AjustarPosicionSuelo();
    }
    private void AjustarPosicionParedes() //Ajuste paredes
    {
        Vector3 screenRight = new Vector3(Screen.width, Screen.height/2, Camera.main.nearClipPlane);
        Vector3 worldRight = Camera.main.ScreenToWorldPoint(screenRight);

        Vector3 screenLeft= new Vector3(0, Screen.height / 2, Camera.main.nearClipPlane);
        Vector3 worldLeft = Camera.main.ScreenToWorldPoint(screenLeft);

        rightWall.transform.position = worldRight + new Vector3(1.5f,0,0);
        leftWall.transform.position = worldLeft + new Vector3(-1.5f,0, 0);
    }

   
}
