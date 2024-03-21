using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class Extensions
{
    public static void CalculateDistance(this Transform unit, Transform point, out float distance) {
        distance = (float) Math.Sqrt(Math.Pow(unit.position.x - point.position.x,2) + Math.Pow(unit.position.y - point.position.y,2));
    }

    public static bool TargetVisibility(this Transform transform, Vector3 targetPos, string tag) {
        Physics.Raycast(new Ray(transform.position, targetPos - transform.position), out RaycastHit hit, 299);
        //Debug.DrawRay(transform.position, targetPos - transform.position,Color.red, 1);
        if (hit.transform.CompareTag(tag)) {
            return true;
        }
        return false;
    }

    public static bool TargetDistance(this Transform transform, Vector3 targetPos, float maxRange) {
        if (Vector3.Distance(transform.position, targetPos) < maxRange) {
            //in range
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
    }public static T[] ShuffleArray<T>(T[] array) {
        System.Random rn = new System.Random();
        for (int i = 0; i < array.Length-1; i++) {
            int randomIndex = rn.Next(i, array.Length);
            T temp = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = temp;
        }
        return array;
    }

    public static Vector3 GetRotatedVector3(this Vector3 point,int count, int index) {
            float angle = index * (360f / count);
            Vector3 dir = ApplyRotationVector(new Vector3(1, 0, 1), angle);
            return point + dir * 1.5f;
    }
    static Vector3 ApplyRotationVector(Vector3 vec, float angle) {
        return Quaternion.Euler(0, angle, 0) * vec;
    }
}
