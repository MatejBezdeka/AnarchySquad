using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackUnitState : UnitState {
    Unit target;
    NavMeshAgent targetAgent;
    SquadUnit unit;
    NavMeshAgent unitAgent;
    bool conditionsMet = false;
    public AttackUnitState(SquadUnit unit, NavMeshAgent unitAgent,Unit target) : base(unit) {
        this.target = target;
        targetAgent = target.gameObject.GetComponent<NavMeshAgent>();
        this.unitAgent = unitAgent;
        this.unit = unit;
    }
    protected override void Enter() {
        //StartCoroutine(CheckConditions());
        base.Enter();
        unit.weapon.LockOn(target.gameObject, targetAgent, unit, unitAgent, unit.muzzle);
    }

    protected override void UpdateState() {
        unit.weapon.Update();
    }

    IEnumerator CheckConditions() {
        WaitForSeconds waitTime = new WaitForSeconds(0.2f);
        while (!conditionsMet) {
            if (!unit.transform.TargetDistance(target.transform.position, unit.weapon.EffectiveRange)) {
                yield return waitTime;
            }
            if (!unit.transform.TargetVisibility(target.transform.position, "Anarchist")) {
                yield return waitTime;
            }
            conditionsMet = true;
        }
    }
    protected override void Exit(UnitState state) {
        unit.weapon.LockOff();
        base.Exit(state);
    }
    
}
