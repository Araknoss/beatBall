using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsValueText;
    [SerializeField] private TextMeshProUGUI pointsCostHpText;
    [SerializeField] private TextMeshProUGUI pointsCostNextText;
    [SerializeField] private PulseToTheBeat pulseToTheBeatPoints;
    [SerializeField] private PulseToTheBeat pulseToTheBeatHpPoints;
    [SerializeField] private PulseToTheBeat pulseToTheBeatNextPoints;
    [SerializeField] private PulseToTheBeat pulseToTheBeatHpNumber;
    [SerializeField] private PulseToTheBeat pulseToTheBeatNextNumber;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float volume;

    [SerializeField] private float pointsTime;
    //[SerializeField] private float startTime;

    [SerializeField] private CanvasGroup canvasGroup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StaticStats.points += 20;
            pointsValueText.text = StaticStats.points.ToString("");
        }
    }
    private void Start()
    {
        //Puntos
        pointsValueText.text = StaticStats.points.ToString("");

        //Coste puntos para mejorar HP
        HpPointsCost();

        //Coste puntos para mejorar Next
        NextPointsCost();

        canvasGroup.blocksRaycasts = true;
    }
    public void RestHpPoints()
    {
        if (StaticStats.points >= StaticStats.pointsHpCost)
        {
            canvasGroup.blocksRaycasts = false;
            StartCoroutine(RestHpPointsCoroutine());
        }       
    }

    public void RestNextPoints()
    {
        if(StaticStats.points >= StaticStats.pointsNextCost)
        {
            canvasGroup.blocksRaycasts = false;
            StartCoroutine(RestNextPointsCoroutine());
        }
    }

    IEnumerator RestHpPointsCoroutine()
    {
        
        float iterationTime = pointsTime / StaticStats.pointsHpCost;
        for (int i = 0; i < StaticStats.pointsHpCost; i++)
        {
            pulseToTheBeatPoints.Pulse();
            StaticStats.points -= 1;
            pointsValueText.text = StaticStats.points.ToString("");
            AudioSource.PlayClipAtPoint(clip, Vector3.zero, volume);
            yield return new WaitForSeconds(iterationTime);

        }
        StaticStats.extraPointsHpCost = StaticStats.pointsHpCost + 50;
        HpPointsCost();
        pulseToTheBeatHpPoints.Pulse();
        yield return new WaitForSeconds(0.5f);
        pulseToTheBeatHpNumber.Pulse();
        canvasGroup.blocksRaycasts = true;

    }

    IEnumerator RestNextPointsCoroutine()
    {
        float iterationTime = pointsTime / StaticStats.pointsNextCost;
        for (int i = 0; i < StaticStats.pointsNextCost; i++)
        {
            pulseToTheBeatPoints.Pulse();
            StaticStats.points -= 1;
            pointsValueText.text = StaticStats.points.ToString("");
            AudioSource.PlayClipAtPoint(clip, Vector3.zero, volume);
            yield return new WaitForSeconds(iterationTime); //para reducir el tiempo cuando tienes muchos puntos

        }
        StaticStats.extraPointsNextCost += 50;
        NextPointsCost();
        pulseToTheBeatNextPoints.Pulse();
        yield return new WaitForSeconds(0.5f);
        pulseToTheBeatNextNumber.Pulse();
        canvasGroup.blocksRaycasts = true;
    }

    private void HpPointsCost()
    {
        StaticStats.pointsHpCost = StaticStats.extraPointsHpCost + StaticStats.initialPointsHpCost;
        pointsCostHpText.text = StaticStats.pointsHpCost.ToString("");
    }

    private void NextPointsCost()
    {
        StaticStats.pointsNextCost = StaticStats.extraPointsNextCost + StaticStats.initialPointsNextCost;
        pointsCostNextText.text = StaticStats.pointsNextCost.ToString("");
    }
}
