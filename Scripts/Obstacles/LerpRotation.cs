using UnityEngine;

public class SmoothContinuousRotation : MonoBehaviour
{
    public bool right;
    public Vector3 rotationAxis = Vector3.up; // Eje alrededor del cual rotar
    public float rotationSpeed = 30f; // Velocidad de rotación
    public float smoothness = 5f; // Suavidad de la rotación

    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        if (right)
        {
            targetRotation *= Quaternion.Euler(rotationAxis * rotationSpeed * Time.deltaTime);
        }
        else
        {
            targetRotation *= Quaternion.Euler(rotationAxis * -rotationSpeed * Time.deltaTime);
        }
        

        // Aplica suavizado a la rotación
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothness * Time.deltaTime);
    }
}


