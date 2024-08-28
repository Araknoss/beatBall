using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f; // Velocidad de rotación en grados por segundo
    [SerializeField] private bool right;
    void Update()
    {
        if (right)
        {
            // Rotar el objeto sobre el eje Z en el tiempo
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }
        
    }
}


