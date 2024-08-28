using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinamicBackground : MonoBehaviour
{
    [SerializeField] private GameObject square;
    [SerializeField] private GameObject smallSquare;
    [SerializeField] private GameObject bigSquare;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerateSquare", 0, 3);
        InvokeRepeating("GenerateBigSquare", 1, 3);
        InvokeRepeating("GenerateSmallSquare", 2, 3);
    }
       
    void GenerateSquare()
    {
        Instantiate(square, transform);
    }

    void GenerateBigSquare()
    {
        Instantiate(bigSquare, transform);
    }

    void GenerateSmallSquare()
    {
        Instantiate(smallSquare, transform);
    }


}
