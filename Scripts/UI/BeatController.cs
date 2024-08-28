using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class BeatController : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Intervals[] intervals;
    

    private void Update()
    {
        foreach(Intervals interval in intervals)
        {
            float sampledTime = (audioSource.timeSamples / audioSource.clip.frequency * interval.GetBeatLength(bpm));
            interval.CheckForNewInterval(sampledTime);
        }
             
    }

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    [System.Serializable]
    public class Intervals
    {
        [SerializeField] private float steps;
        [SerializeField] private UnityEvent trigger;
        private int lastInterval;

        public float GetBeatLength(float bpm)
        {
            return 60f / (bpm * steps); //Calculamos beats por segundo pudiendo modificar la variable steps conforme nos interese
        }

        public void CheckForNewInterval(float interval)
        {
            if (Mathf.FloorToInt(interval) != lastInterval)
            {
                lastInterval = Mathf.FloorToInt(interval);
                trigger.Invoke();
            }
        }
    }
}
