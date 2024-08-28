using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveAnim : MonoBehaviour
{
    [SerializeField] private int collidesCounter;

    [SerializeField] private Animator animator;
    [SerializeField] private PruebaMovimiento pruebaMovimiento;

    [Header("Portal")]
    [SerializeField] private Animator portalAnimator;
    
        
  
    private void Start()
    {
        ResetReact();
    }
    private void Update()
    {
        if (pruebaMovimiento.resetCounter == true)
        {
            ResetReact();
        }
    }
    public void React()
    {
        StaticStats.reactiveCollides += 1;
        if (StaticStats.reactiveCollides >= collidesCounter)
        {
            portalAnimator.SetTrigger("React");
            portalAnimator.SetBool("Iddle", false);
           
        }
        gameObject.tag = "Untagged";
        PulseToTheBeat pulseToTheBeat = gameObject.GetComponent<PulseToTheBeat>();
        pulseToTheBeat.Pulse();
        animator.SetBool("Iddle",false);
        animator.SetTrigger("React");
       
    }


    public void ResetReact()
    {
        StaticStats.reactiveCollides = 0;
        gameObject.tag = "Reactive";
        animator.SetBool("Iddle", true);

        
        portalAnimator.SetBool("Iddle", true);
       
    }

}
