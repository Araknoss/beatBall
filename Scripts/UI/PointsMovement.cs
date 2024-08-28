using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private TMP_Text collidePointsText;

    [Header("Position")]    
    private Vector3 startPosition;
    private Vector3 targetPosition;
    [SerializeField] public Vector3 targetPositionOffset;
    [SerializeField] private float lerpDuration;

    [Header("Rotation")]    
    private Quaternion startRotation;
    [SerializeField] private Vector3 targetRotation;

    [Header("Color")]    
    private Color startColor;
    [SerializeField] Color targetColor;

   

    private void Start()
    {
        collidePointsText.text = StaticStats.collidePoints.ToString("");
        startPosition = transform.position;
        targetPosition = startPosition + targetPositionOffset;
        startRotation = transform.rotation;
        
        startColor = collidePointsText.color;
        StartCoroutine(LerpPosition(startPosition, targetPosition, lerpDuration));
        StartCoroutine(LerpRotation(startRotation, Quaternion.Euler(targetRotation), lerpDuration));
        StartCoroutine(LerpColor(startColor, targetColor, lerpDuration));
        StartCoroutine(Destroy(lerpDuration));
    }
   
    IEnumerator Destroy(float lerpDuration)
    {
        yield return new WaitForSeconds(lerpDuration);
        Destroy(gameObject);
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
       

    }
    IEnumerator LerpRotation(Quaternion start, Quaternion target, float lerpDuration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            transform.rotation = Quaternion.Lerp(start, target, curve.Evaluate(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = target; //Le asignamos valor final ya que time deltaTime no es exacto y nunca nos va a dar el número entero
        //SwitchTarget();

    }

    IEnumerator LerpColor(Color start, Color target, float lerpDuration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            collidePointsText.color = Color.Lerp(start, target, curve.Evaluate(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        collidePointsText.color = target; //Le asignamos valor final ya que time deltaTime no es exacto y nunca nos va a dar el número entero
        //SwitchTarget();

    }
}
