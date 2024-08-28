using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground : MonoBehaviour
{
    private Camera mainCamera;
    private Transform ball;
    public float ratioCoefficient;
    

    private void Awake()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenRatio = screenWidth / screenHeight;
        float actualRatio = 2f; //2160x1080 ratio
        ratioCoefficient = screenRatio / actualRatio;

        
        transform.localScale *= ratioCoefficient;

        

    }
}
