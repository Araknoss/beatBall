using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCirclesRotate : MonoBehaviour
{
    private float timer;
    [SerializeField] private bool right;
    [SerializeField] private GameObject bCircle1;
    [SerializeField] private GameObject bCircle2;
    private void Start()
    {
        StartCoroutine(CRotateLeft());
        timer = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            if (right)
            {
                StartCoroutine(CRotateLeft());
            }
            else
            {
                StartCoroutine(CRotateRight());
            }
            timer = 0;
        }

    }

    IEnumerator CRotateLeft()
    {
        transform.LeanRotate(new Vector3(0f, 0f, 180f), 1).setEaseOutBack();
        right = false;
        yield return new WaitForSeconds(1f);
        bCircle1.transform.LeanScale(Vector3.one * 0.5f, 1).setEaseOutBack();
        bCircle2.transform.LeanScale(Vector3.one, 1).setEaseOutBack();
        yield return null;
        
    }
    IEnumerator CRotateRight()
    {
        transform.LeanRotate(new Vector3(0f, 0f, 0f), 1).setEaseOutBack();
        right = true;
        yield return new WaitForSeconds(1f);
        bCircle1.transform.LeanScale(Vector3.one, 1).setEaseOutBack();
        bCircle2.transform.LeanScale(Vector3.one*0.5f, 1).setEaseOutBack();
        yield return null;
    }
   /* public void RotateLeft()
    {
        transform.LeanRotate(new Vector3(0f, 0f, 180f), 1).setEaseOutBack();
        right = false;
    }
    public void RotateRight()
    {
        transform.LeanRotate(new Vector3(0f, 0f, 0f), 1).setEaseOutBack();
        right = true;
    }*/
}
