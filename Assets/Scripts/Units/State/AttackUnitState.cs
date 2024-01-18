using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;

public class AttackUnitState : UnitState {
    Unit target;
    bool conditionsMet = false;
    bool attacking = false;
    float currentCooldown = 0;
    int currentBurst = 0;
    public AttackUnitState(SquadUnit unit, Unit target) : base(unit) {
        this.target = target;
    }
    protected override void Enter() {
        //StartCoroutine(CheckConditions());
        unit.needToReload += Reload;
        unit.SetTarget(target);
        base.Enter();
    }

    protected override void UpdateState() {
        currentCooldown += Time.deltaTime;
        unit.weapon.UpdateWeapon(unit, target, ref attacking,ref currentBurst, ref currentCooldown);
    }

    IEnumerator CheckConditions() {
        WaitForSeconds waitTime = new WaitForSeconds(0.2f);
        while (!conditionsMet) {
            if (!unit.transform.TargetDistance(target.transform.position, unit.weapon.EffectiveRange)) {
                unit.Agent.SetDestination(target.transform.position);
                yield return waitTime;
            }
            if (!unit.transform.TargetVisibility(target.transform.position, "Anarchist")) {
                unit.Agent.SetDestination(target.transform.position);
                yield return waitTime;
            }
            unit.Agent.SetDestination(unit.transform.position);
            conditionsMet = true;
        }
    }

    void Reload(float reloadTime) {
        Exit(new ReloadUnitState(unit, reloadTime, this));
    }
    protected override void Exit(UnitState state) {
        base.Exit(state);
    }
    
}
