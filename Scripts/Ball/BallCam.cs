using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCam : MonoBehaviour
{
    
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float startSize;
    [SerializeField] private float targetSize;
    [SerializeField] private float maxHeight;
    [SerializeField] private PruebaMovimiento pruebaMovimiento;
    // Start is called before the first frame update
    void Start()
    {
       Camera.main.orthographicSize = startSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (pruebaMovimiento.transitioning == false)
        {
            Camera.main.orthographicSize = Mathf.Lerp(startSize, targetSize, curve.Evaluate(transform.position.y / maxHeight));
        }
       
    }
}
