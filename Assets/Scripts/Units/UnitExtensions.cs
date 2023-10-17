using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitExtensions
{
    public static float CalculateDistance(this Transform unit, Transform enemy) {
        return (float) 
            Math.Sqrt(Math.Pow(unit.position.x - enemy.position.x,2) + 
                      Math.Pow(unit.position.y - enemy.position.y,2));
    }
}
