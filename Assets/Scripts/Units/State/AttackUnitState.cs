using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AttackUnitState : UnitState {
    Unit target;
    bool attacking = false;
    float currentCooldown = 0;
    int currentBurst = 0;
    public AttackUnitState(Unit unit, Unit target) : base(unit) {
        this.target = target;
    }
    protected override void Enter() {
        //StartCoroutine(CheckConditions());
        unit.needToReload += Reload;
        base.Enter();
    }

    protected override void UpdateState() {
        if (CheckConditions()) {
            unit.transform.Rotate(Vector3.RotateTowards(unit.transform.forward, target.transform.position - unit.transform.position, 5, 5));
            currentCooldown += Time.deltaTime;
            unit.weapon.UpdateWeapon(unit, target, ref attacking,ref currentBurst, ref currentCooldown);
        }
    }

    bool CheckConditions() {
            if (!unit.transform.TargetDistance(target.transform.position, unit.weapon.EffectiveRange)) {
                Debug.Log(target.transform.position);
                unit.Agent.SetDestination(target.transform.position);
                currentCooldown /= 2;
                return false;
            }
            if (!unit.transform.TargetVisibility(target.transform.position, "Anarchist")) {
                unit.Agent.SetDestination(target.transform.position);
                currentCooldown /= 2;
                return false;
            }
            unit.Agent.ResetPath();
            return true;
    }

    void Reload(float reloadTime) {
        Exit(new ReloadUnitState(unit, this));
    }
    protected override void Exit(UnitState state) {
        unit.Agent.ResetPath();
        base.Exit(state);
    }
    
}
