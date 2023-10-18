using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit {
    private SquadUnit closestEnemy = null;
    private float closestDistance = float.MaxValue;
    // Start is called before the first frame update
    void Start() {
//         StartCoroutine(SlowUpdate());
    }
    IEnumerator SlowUpdate() {
        WaitForSeconds waitTime = new WaitForSeconds(responseTime);
        while (true) {
            if (GetClosestEnemy()) {
                behavior.Shoot(closestEnemy, closestEnemy.GetMoveVector(), closestDistance);
            }
            yield return waitTime;
        }
    }

    SquadUnit GetClosestEnemy() {
        foreach (var unit in GameManager.instance.Squaders) {
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
        base.Die();
    }
}
