using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit {
    private SquadUnit closestEnemy = null;
    private float closestDistance = float.MaxValue;
    float morale;
    void Start() {
        base.Start();
//         StartCoroutine(SlowUpdate());
    }
    IEnumerator SlowUpdate() {
        WaitForSeconds waitTime = new WaitForSeconds(responseTime);
        while (true) {
            if (GetClosestEnemy()) {
                //weapon.Shoot(closestEnemy, closestEnemy.GetMoveVector(), closestDistance);
            }
            yield return waitTime;
        }
    }

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
        StopCoroutine(SlowUpdate());
        //add points?
        //check if game over
        //death sound
        base.Die();
        
    }

    public override bool isSquadUnit() {
        return false;
    }
}
