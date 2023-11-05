using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;

public class AttackUnitState : UnitState {
    Unit target;
    SquadUnit unit;
    bool conditionsMet = false;
    public AttackUnitState(SquadUnit unit, Unit target) : base(unit) {
        this.target = target;
        this.unit = unit;
    }
    protected override void Enter() {
        //StartCoroutine(CheckConditions());
        base.Enter();
        unit.weapon.LockOn(target, unit, unit.muzzle);
        unit.weapon.needToReload += Reload;
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

    void Reload(float reloadTime) {
        Exit(new ReloadUnitState(unit, reloadTime, this));
    }
    protected override void Exit(UnitState state) {
        unit.weapon.LockOff();
        base.Exit(state);
    }
    
}
