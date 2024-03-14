using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyUnit : Unit {
    private SquadUnit closestEnemy = null;
    private float closestDistance = float.MaxValue;
    float morale;
    void Start() {
        base.Start();
        //StartCoroutine(SlowUpdate());
    }
    /*IEnumerator SlowUpdate() {
        WaitForSeconds waitTime = new WaitForSeconds(responseTime);
        while (true) {
            if () {
                //weapon.Shoot(closestEnemy, closestEnemy.GetMoveVector(), closestDistance);
            }
            yield return waitTime;
        }
    }*/

    SquadUnit GetClosestEnemy() {
        foreach (var unit in GameManager.instance.Units) {
            unit.transform.CalculateDistance(transform, out float distance);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestEnemy = unit;
                return closestEnemy;
            }
        }
        return closestEnemy = null;
    }

    protected override void Die() {
        //StopCoroutine(SlowUpdate());
        //add points?
        //check if game over
        //death sound
        base.Die();
        
    }

    public override bool isSquadUnit() {
        return false;
    }

    void UpdateClosestEnemy() {
        float distance = float.MaxValue;
        float currentDistance = 0;
        SquadUnit currentClosest;
        NavMeshPath path = new NavMeshPath();
        foreach (var unit in GameManager.instance.Units) {
            NavMesh.CalculatePath(transform.position, unit.transform.position, NavMesh.AllAreas, path);
            for ( int i = 1; i < path.corners.Length; ++i ) { 
                currentDistance += Vector3.Distance( path.corners[i-1], path.corners[i] );
            }

            if (currentDistance < distance) {
                distance = currentDistance;
                currentClosest = unit;
            }
        }
        closestDistance = currentDistance;
    }
}
