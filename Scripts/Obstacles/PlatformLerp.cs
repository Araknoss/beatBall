using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLerp : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    [SerializeField] private Vector3 displacement;
    [SerializeField] private float lerpDuration;
    

    void Start()
    {
        startPosition = transform.position;
        targetPosition = displacement + startPosition;
        StartCoroutine(LerpPosition(startPosition, targetPosition, lerpDuration));
    }

    IEnumerator LerpPosition(Vector3 start, Vector3 target, float lerpDuration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            transform.position = Vector3.Lerp(start, target, curve.Evaluate(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target; //Le asignamos valor final ya que time deltaTime no es exacto y nunca nos va a dar el número entero
        SwitchTarget();

    }
    
    private void SwitchTarget()
    {
        targetPosition = startPosition;
        startPosition = transform.position;
        StartCoroutine(LerpPosition(startPosition, targetPosition, lerpDuration));
    }
   
}
