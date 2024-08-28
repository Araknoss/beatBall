using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;

    [Header("Position")]
    [SerializeField] public bool position;
    private Vector3 startPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float lerpDuration;
    
    [Header("Rotation")]
    [SerializeField] public bool rotation;
    private Quaternion startRotation;
    [SerializeField] private Vector3 targetRotation;

    [Header("Color")]
    [SerializeField] public bool color;
    private SpriteRenderer sr;
    private Color startColor;
    [SerializeField] Color targetColor;



    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.material.color;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (position == true)
            {
                StartCoroutine(LerpPosition(startPosition, targetPosition, lerpDuration));
            }
            if (rotation == true)
            {
                StartCoroutine(LerpRotation(startRotation, Quaternion.Euler(targetRotation), lerpDuration));
            }
            if (color == true)
            {
                StartCoroutine(LerpColor(startColor, targetColor, lerpDuration));
            }
            
        }
    }

    IEnumerator LerpPosition(Vector3 start, Vector3 target,float lerpDuration)
    {
        float timeElapsed=0f;
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
       
        targetColor = startColor;
        startColor = sr.material.color;
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
            sr.material.color = Color.Lerp(start, target, curve.Evaluate(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        sr.material.color = target; //Le asignamos valor final ya que time deltaTime no es exacto y nunca nos va a dar el número entero
        //SwitchTarget();

    }
}
