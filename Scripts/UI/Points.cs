using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI pointsValueText;
    [SerializeField] private TextMeshProUGUI collidePointsValueText;
    [SerializeField] private PulseToTheBeat pointsPulseToTheBeat;
    [SerializeField] private PulseToTheBeat collidePointsPulseToTheBeat;
    //[SerializeField] private int pointsEarned;
    [SerializeField] private float pointsTime;
    [SerializeField] private float startTime;

    private void Start()
    {
        ResetPoints();
        pointsValueText.text = StaticStats.points.ToString("");
    }
    public void UpPoints()
    {   
        animator.SetTrigger("Down");
        StartCoroutine(UpPointsCoroutine());
        //StartCoroutine(RestCollidePointsCoroutine());
    }

    public void ResetPoints()
    {
        //StaticStats.collideCounter = 0;
        //StaticStats.collidePoints = 0;
        StaticStats.collideTotalPoints = 5;
        //collidePointsValueText.text = StaticStats.collideTotalPoints.ToString("");
    }
   /* public void UpCollisionPoints()
    {
        StaticStats.collideCounter += 1;
        StaticStats.collidePoints -= StaticStats.collideCounter;
        StaticStats.collideTotalPoints += StaticStats.collidePoints;
        collidePointsPulseToTheBeat.Pulse();
        collidePointsValueText.text = StaticStats.collideTotalPoints.ToString("");

    }*/
    
    public void CleanShot()
    {
        StaticStats.collideTotalPoints += 5;
        //collidePointsPulseToTheBeat.Pulse();
        //collidePointsValueText.text = StaticStats.collideTotalPoints.ToString("");
    }



    IEnumerator UpPointsCoroutine()
    {
        float iterationTime = pointsTime / StaticStats.collideTotalPoints; //Tiempo entre iteraciones del bucle
        yield return new WaitForSeconds(startTime);
        for (int i = 0; i < StaticStats.collideTotalPoints; i++)
        {
            pointsPulseToTheBeat.Pulse();
            StaticStats.points += 1;
            pointsValueText.text = StaticStats.points.ToString("");
            yield return new WaitForSeconds(iterationTime);
        }

        


    }

    IEnumerator RestCollidePointsCoroutine()
    {
        float iterationTime = pointsTime / StaticStats.collideTotalPoints; //Tiempo entre iteraciones del bucle
        yield return new WaitForSeconds(startTime);
        int restingPoints = StaticStats.collideTotalPoints; //Variable para restar valores a collidePoints sin restar la variable estatica
        for (int i = 0; i < StaticStats.collideTotalPoints; i++)
        {
            pointsPulseToTheBeat.Pulse();
            restingPoints -= 1;
            collidePointsValueText.text = restingPoints.ToString("");
            yield return new WaitForSeconds(iterationTime);
        }
    }
}
