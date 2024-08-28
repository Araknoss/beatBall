using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PulseToTheBeat : MonoBehaviour
{
    [Header("Beat")]
    [SerializeField] bool useTestBeat=true;
    [SerializeField] float pulseSize = 1.15f;
    [SerializeField] float returnSpeed = 5f;
    private Vector3 startSize;
    [SerializeField] private bool maxBpm=false;

    
    private void Start()
    {
        startSize = transform.localScale;
        if (useTestBeat)
        {
            StartCoroutine(TestBeat());
        }
        else if (maxBpm)
        {
            StartCoroutine(TestMaxBeat());
        }
        
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startSize, Time.deltaTime * returnSpeed);
    }
    
    public void Pulse()
    {
        transform.localScale = startSize * pulseSize;
    }


    IEnumerator TestBeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Pulse();
            
        }
    }

    IEnumerator TestMaxBeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Pulse();

        }
    }

}


