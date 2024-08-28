using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryLine : MonoBehaviour
{
	LineRenderer lineRenderer;
	List<Vector3> myPoints;
	
    private void Start()
    {
        myPoints = new List<Vector3>();
        lineRenderer = GetComponent<LineRenderer>();
       
        
    }

    private void Update()
    {
        lineRenderer.positionCount = myPoints.Count;
        for(int i = 0; i < myPoints.Count; i++)
        {
            lineRenderer.SetPosition(i,myPoints[i]);
        }
      
    }
    public void RenderCurve()
    {
        InvokeRepeating("AddPoint", 0.02f, 0.04f);
        InvokeRepeating("RemovePoint", 0.4f, 0.04f);
       
       
    }
    public void EndLine()
    {
       
        CancelInvoke();
        myPoints.Clear();
        lineRenderer.positionCount = 0;

    }
    
    private void RemovePoint()
    {
        
        myPoints.RemoveAt(0);
    }
    private void AddPoint()
    {
        int j = 0;
        List<Vector3> tempPoints = new List<Vector3>();
        if (myPoints != null)
        {
            for (j = 0; j < myPoints.Count; j++)
            {
                tempPoints.Add(myPoints[j]);
            }
            Vector3 tempPos = new Vector3();
            tempPos = Input.mousePosition;
            tempPos.z = 15;
            tempPoints.Add(Camera.main.ScreenToWorldPoint(tempPos));
            myPoints = new List<Vector3>();
            myPoints = tempPoints;
        }
    }
}   
