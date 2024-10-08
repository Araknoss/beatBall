using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activation : MonoBehaviour
{
    [SerializeField] private bool even;
    [SerializeField] private List<GameObject> rythmObjects = new List<GameObject>();
    [SerializeField] private Color activeColor;
    [SerializeField] private Color disableColor;
    [SerializeField] private PruebaMovimiento pm;

    void Start()
    {
        Reset();

    }

    private void Update()
    {
        if (pm.resetCounter == true)
        {
            Reset();
        }
    }

    private void Reset()
    {
        even = false;
        Activate();
    }

    public void Activate()
    {
        if (even)
        {
            StartCoroutine(Even());
            even = false;
        }
        else
        {
            StartCoroutine(Odd());
            even = true;
        }
    }


    IEnumerator Even()
    {

        for (int i = 0; i < rythmObjects.Count; i += 2) //Activación
        {
            SpriteRenderer sr = rythmObjects[i].GetComponent<SpriteRenderer>();
            sr.color = activeColor;
            Collider2D boxCollider = rythmObjects[i].GetComponent<Collider2D>();
            boxCollider.enabled = true;
            PulseToTheBeat pulseToTheBeat = rythmObjects[i].GetComponent<PulseToTheBeat>();
            pulseToTheBeat.Pulse();
        }
        for (int i = 1; i < rythmObjects.Count; i += 2) //Desactivación
        {
            SpriteRenderer sr = rythmObjects[i].GetComponent<SpriteRenderer>();
            sr.color = disableColor;
            Collider2D boxCollider = rythmObjects[i].GetComponent<Collider2D>();
            boxCollider.enabled = false;
            PulseToTheBeat pulseToTheBeat = rythmObjects[i].GetComponent<PulseToTheBeat>();
            pulseToTheBeat.Pulse();
        }
        yield return null;
    }

    IEnumerator Odd()
    {
        for (int i = 0; i < rythmObjects.Count; i += 2)
        {
            SpriteRenderer sr = rythmObjects[i].GetComponent<SpriteRenderer>();
            sr.color = disableColor;
            Collider2D boxCollider = rythmObjects[i].GetComponent<Collider2D>();
            boxCollider.enabled = false;
            PulseToTheBeat pulseToTheBeat = rythmObjects[i].GetComponent<PulseToTheBeat>();
            pulseToTheBeat.Pulse();
        }
        for (int i = 1; i < rythmObjects.Count; i += 2)
        {
            SpriteRenderer sr = rythmObjects[i].GetComponent<SpriteRenderer>();
            sr.color = activeColor;
            Collider2D boxCollider = rythmObjects[i].GetComponent<Collider2D>();
            boxCollider.enabled = true;
            PulseToTheBeat pulseToTheBeat = rythmObjects[i].GetComponent<PulseToTheBeat>();
            pulseToTheBeat.Pulse();
        }
        yield return null;
    }
}
