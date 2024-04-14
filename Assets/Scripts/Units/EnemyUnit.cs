using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyUnit : Unit {
    enum walkState {
        standing,
        goingTo,
        reatreat,
        backUp
    }

    private walkState currentWalkState;
    public SquadUnit closestEnemy { get; private set; } = null;
    public float closestDistance { get; private set; } = float.MaxValue;
    const float maxMorale = 200;
    float morale = 100;

    public float Morale {
        get { return morale; }
        set {
            morale = value;
            if (morale > maxMorale) {
                morale = maxMorale;
            }else if (morale < 0) {
                morale = 0;
            }
                
        }
    }
    float moraleLoseDistance;

    protected override void Start() {
        currentState = new EnemyNormalState(this);
        base.Start();
        StartCoroutine(SlowUpdate());
        if (10 > (weapon.EffectiveRange * 0.75f) || 40 < (weapon.EffectiveRange * 0.75f)) {
            moraleLoseDistance = 10;
        }
        else {
            moraleLoseDistance = weapon.EffectiveRange * 0.75f;
        }
        currentWalkState = walkState.standing;
    }
    
    IEnumerator SlowUpdate() {
        WaitForSeconds waitTime = new WaitForSeconds(responseTime);
        while (true) {
            morale += 0.25f;
            UpdateClosestEnemy();
            if (agent.pathStatus == 0) {
                currentWalkState = walkState.standing;
            }
            yield return waitTime;
        }
    }
    protected override void Die() {
        //StopCoroutine(SlowUpdate());
        //add points?
        //check if game over
        //death sound
        StopCoroutine(SlowUpdate());
        base.Die();
    }

    public override void GetHit(int damage) {
        base.GetHit(damage);
        morale -= CurrentHp < stats.MaxHp / 2 ? 10 : 5;
    }

    public override bool isSquadUnit() {
        return false;
    }

    void UpdateClosestEnemy() {
        float distance = float.MaxValue;
        float currentDistance = 0;
        NavMeshPath path = new NavMeshPath();
        foreach (var unit in GameManager.instance.Units) {
            currentDistance = 0;
            NavMesh.CalculatePath(transform.position, unit.transform.position, NavMesh.AllAreas, path);
            for ( int i = 1; i < path.corners.Length; ++i ) {
                currentDistance += Vector3.Distance( path.corners[i-1], path.corners[i]);
                
            }
            if (closestDistance < moraleLoseDistance) {
                Morale--;
            }
            if (currentDistance < distance) {
                distance = currentDistance;
                closestEnemy = unit;
                if (distance > moraleLoseDistance) {
                    Morale += 1.25f;
                }
            }
        }
        closestDistance = currentDistance;
        //Debug.Log(closestEnemy);
    }

    float DifficultyNormalize(int difficulty) {
        return (float)((Mathf.Log(difficulty)) / 2.5 * difficulty + 1);
    }

    public void Chill() {
        morale += 0.2f;
    }

    public void SetDestinationToSafety() {
        Vector3 closestPosition = this.transform.position;
        float distance = float.MaxValue;
        float currentDistance = 0;
        NavMeshPath path = new NavMeshPath();
        foreach (Vector3 position in GameManager.instance.MapGenerator.ViableSpawnPositionses) {
            currentDistance = 0;
            for ( int i = 1; i < path.corners.Length; ++i ) {
                currentDistance += Vector3.Distance( path.corners[i-1], path.corners[i]);
                
            }
            if (distance > currentDistance) {
                distance = currentDistance;
                closestPosition = position;
            }
        }

        agent.SetDestination(closestPosition);
    }

    public void SetDestinationToSaferPlace() {
        agent.SetDestination(GameManager.instance.MapGenerator.GetSaferPosition(transform.position));
    }
}
