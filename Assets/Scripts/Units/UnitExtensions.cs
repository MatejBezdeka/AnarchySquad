using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitExtensions
{
    public static void CalculateDistance(this Transform unit, Transform point, out float distance) {
        distance = (float) Math.Sqrt(Math.Pow(unit.position.x - point.position.x,2) + Math.Pow(unit.position.y - point.position.y,2));
    }
}
