using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cámara : MonoBehaviour

{
    [SerializeField] private Transform personaje;
    private float tamañoCamara;
    private float alturaPantalla;
    // Start is called before the first frame update
    void Start()
    {
        tamañoCamara = Camera.main.orthographicSize; /*Distancia desde el centro al borde*/
        alturaPantalla = tamañoCamara * 2;
    }

    // Update is called once per frame
    void Update()
    {
        CalcularPosicionCamara();

    }

    void CalcularPosicionCamara()
    {
        int pantallaPersonaje = (int)(personaje.position.y / alturaPantalla);
        float alturaCamara = (pantallaPersonaje * alturaPantalla) + tamañoCamara-4; //Meto un offset de 4 para que se ajuste a mi escena
        transform.position = new Vector3(transform.position.x, alturaCamara, transform.position.z);
    }
}
