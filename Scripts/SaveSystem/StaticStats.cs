using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaticStats
{
    public static int hP;
    public static int next;
    public static int recordLevel;
    public static int initialHp = 10;
    public static int extraHp;
    public static int initialNext = 2;
    public static int extraNext;
    public static int points;
    public static int initialPointsHpCost = 50;
    public static int extraPointsHpCost;
    public static int pointsHpCost;
    public static int initialPointsNextCost = 100;
    public static int extraPointsNextCost;
    public static int pointsNextCost;
    public static int record;
    public static int initialRecord;
    //public static int collideCounter; //Numero de colisiones contra paredes tag "Walls"
    public static int collidePoints; //Puntos por impacto
    public static int collideTotalPoints; //Puntos totales por tirada
    public static int reactiveCollides;
    public static int comboCounter=0;
    public static bool infiniteHp=false;

    
    
}
