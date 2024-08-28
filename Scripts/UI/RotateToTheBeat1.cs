using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RotateToTheBeat : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private bool useTestRotation=true;
    [SerializeField] private float returnSpeed;
    [SerializeField] private Vector3 pulseRotation;
    private Quaternion startRotation;
    [SerializeField] private bool rotateRight;

    private void Start()
    {
        startRotation = transform.rotation;
        if (useTestRotation)
        {
            StartCoroutine(TestRotate());
        }

    }

    private void Update()
    {
        if (rotateRight)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, Time.deltaTime * returnSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, Time.deltaTime * -returnSpeed);
        }
        
    }
      
    public void Rotate()
    {
        
            transform.rotation = startRotation * Quaternion.Euler(pulseRotation);
        
    }

    IEnumerator TestRotate()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(1f);
            Rotate();
            
        }
    }
   
}


