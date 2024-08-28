using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmPortalUpgrades : MonoBehaviour
{
    [SerializeField] private Vector3 position1;
    [SerializeField] private Vector3 position2;
    private float timer;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationCurve effectCurve;

    [Header("Children")]
    [SerializeField] private GameObject goalEffect;
    [SerializeField] private ParticleSystem attractionEffect;
    [SerializeField] private GameObject portalMask;
    [SerializeField] private GameObject hP;
    [SerializeField] private GameObject next;
    private bool currentPosition;
    private bool goal;

    private void Start()
    {
        SwitchPosition();
    }


    public void Goal()
    {
        goal = true;
        animator.SetBool("Open", false);
        animator.SetTrigger("Close");
        goalEffect.SetActive(true);
        portalMask.SetActive(true);
        StartCoroutine(LerpEffect(10, 0, 1));
   
    }

    public void ResetPortal()
    {
        timer = 0;
        goal = false;
        animator.SetBool("Open",true);
        goalEffect.SetActive(false);
        portalMask.SetActive(false);
        var emission = attractionEffect.emission;
        emission.rateOverTimeMultiplier = 10;
    }

    IEnumerator LerpEffect(float start, float target, float lerpDuration) //Cantidad de particulas
    {
        var emission = attractionEffect.emission; //hacemos del parametro emission una variable
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            emission.rateOverTimeMultiplier = Mathf.Lerp(start, target, effectCurve.Evaluate(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        emission.rateOverTime = target; //Le asignamos valor final ya que time deltaTime no es exacto y nunca nos va a dar el n�mero entero
        gameObject.SetActive(false);


        //SwitchTarget();
    }

    private void Update()
    {
        if (goal == false)
        {
            timer += Time.deltaTime;
        }
        
                
        if (timer >= 1)
        {
            SwitchPosition();
            timer = 0;
        }
    }
    public void SwitchPosition()
    {
            if (currentPosition == true)
            {
                    transform.position = position1;
                    hP.SetActive(true);
                    next.SetActive(false);
                    currentPosition = false;
            }
            else
            {
                    transform.position = position2;
                    next.SetActive(true);
                    hP.SetActive(false);
                    currentPosition = true;
            }
             
    }
}
