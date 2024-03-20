using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyUnit : Unit {
    private SquadUnit closestEnemy = null;
    private float closestDistance = float.MaxValue;
    float morale = 100;
    float moraleLoseDistance;
    void Start() {
        base.Start();
        if (7.5f > (weapon.EffectiveRange * 0.75f) || 18 < (weapon.EffectiveRange * 0.75f)) {
            moraleLoseDistance = 10;
        }
        else {
            moraleLoseDistance = weapon.EffectiveRange * 0.75f;
        }
        StartCoroutine(SlowUpdate());
    }
    IEnumerator SlowUpdate() {
        WaitForSeconds waitTime = new WaitForSeconds(responseTime);
        while (true) {
            morale += 0.25f;
            UpdateClosestEnemy();
            yield return waitTime;
        }
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
        NavMeshPath path = new NavMeshPath();
        foreach (var unit in GameManager.instance.Units) {
            NavMesh.CalculatePath(transform.position, unit.transform.position, NavMesh.AllAreas, path);
            for ( int i = 1; i < path.corners.Length; ++i ) { 
                currentDistance += Vector3.Distance( path.corners[i-1], path.corners[i]);
                if (closestDistance < moraleLoseDistance) {
                    morale--;
                }
            }
            if (currentDistance < distance) {
                distance = currentDistance;
                closestEnemy = unit;
                if (distance > moraleLoseDistance) {
                    morale += 3;
                }
            }
        }
        closestDistance = currentDistance;
        Debug.Log(closestEnemy);
    }
}
