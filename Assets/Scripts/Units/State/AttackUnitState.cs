using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AttackUnitState : UnitState {
    Unit target;
    protected bool attacking = false;
    protected float currentCooldown = 0;
    protected int currentBurst = 0;
    bool repeat = true;
    bool conditions;
    public AttackUnitState(Unit unit, Unit target) : base(unit) {
        this.target = target;
    }
    protected override void Enter() {
        unit.needToReload += Reload;
        base.Enter();
        
    }

    protected override void UpdateState() {
        if (CheckConditions()) {
            unit.Agent.ResetPath();
            unit.transform.Rotate(Vector3.RotateTowards(unit.transform.forward, target.transform.position - unit.transform.position, 5, 5));
            currentCooldown += Time.deltaTime;
            unit.weapon.UpdateWeapon(unit, target, ref attacking,ref currentBurst, ref currentCooldown);
        }
    }

    bool CheckConditions() {
        if (target == null) {
            Exit(new NormalUnitState(unit));
        }
        if (!unit.transform.TargetDistance(target.transform.position, unit.weapon.EffectiveRange) || !unit.transform.TargetVisibility(target.transform.position, "Anarchist")) {
            //Debug.Log(target.transform.position);
            unit.Agent.SetDestination(target.transform.position);
            currentCooldown /= 2;
            return false;
        }
        return true;
    }

    void Reload(float reloadTime) {
        Exit(new ReloadUnitState(unit, new AttackUnitState(unit, target)));
    }
    protected override void Exit(UnitState state) {
        unit.Agent.ResetPath();
        repeat = false;
        base.Exit(state);
    }
}
