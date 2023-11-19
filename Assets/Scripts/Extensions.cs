using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void CalculateDistance(this Transform unit, Transform point, out float distance) {
        distance = (float) Math.Sqrt(Math.Pow(unit.position.x - point.position.x,2) + Math.Pow(unit.position.y - point.position.y,2));
    }
    public static bool TargetVisibility(this Transform transform, Vector3 targetPos, string tag) {
        //Debug.Log("visible");
        Physics.Raycast(new Ray( transform.position, targetPos), out RaycastHit hit, 999);
        if (hit.transform.CompareTag(tag)) {
            return true;
        }
        //Obstacle in way
        return false;
    }

    public static bool TargetDistance(this Transform transform, Vector3 targetPos, float maxRange) {
        //Debug.Log("distancing");
        if (Vector3.Distance(transform.position, targetPos) < maxRange) {
            return true;
        }
        //out of distance
        return false;
    }

    public static T[] ShuffleArray<T>(T[] array, int seed) {
        System.Random rn = new System.Random(seed);
        for (int i = 0; i < array.Length-1; i++) {
            int randomIndex = rn.Next(i, array.Length);
            
            T temp = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = temp;
        }
        return array;
    }
}
