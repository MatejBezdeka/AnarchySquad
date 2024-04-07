using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : AttackUnitState
{
    EnemyUnit unit;
    Unit target;
    public EnemyAttackState(Unit unit, Unit target) : base(unit, target) {
        this.unit = unit as EnemyUnit;
        this.target = target;
        //low morale -> grenade/fight and slowly/(quickly)reatreat if no ammo
        //med -> stand your ground while you have ammo than you might have to cover
        //high -> stand ground/push back up a bit when reloading ot low ammo
    }

    protected override void Enter() {
        base.Enter();
    }

    protected override void UpdateState() {
        if (CheckConditions()) {
            unit.transform.Rotate(Vector3.RotateTowards(unit.transform.forward, target.transform.position - unit.transform.position, 5, 5));
            currentCooldown += Time.deltaTime;
            unit.weapon.UpdateWeapon(unit, target, ref attacking,ref currentBurst, ref currentCooldown);
        }
    }

    protected override void Exit(UnitState state) {
        base.Exit(state);
    }
    bool CheckConditions() {
        float targetDistance = Vector3.Distance(unit.transform.position, target.transform.position);
        if (targetDistance > unit.weapon.MaxEffectiveRange) {
            Debug.Log(target.transform.position);
            unit.Agent.SetDestination(target.transform.position);
            currentCooldown /= 2;
            return false;
        }
        if (!unit.transform.TargetVisibility(target.transform.position, "Squader")) {
            unit.Agent.SetDestination(target.transform.position);
            currentCooldown /= 2;
            return false;
        }
        if (targetDistance < unit.weapon.EffectiveRange/3) {
            unit.SetDestinationToSaferPlace();
            return true;
        }
        unit.Agent.ResetPath();
        return true;
    }
}
