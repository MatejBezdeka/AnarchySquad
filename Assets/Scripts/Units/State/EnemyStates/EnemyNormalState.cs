using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalState : NormalUnitState {
    EnemyUnit unit;
    int chillCooldown = 1;
    float currentChillCooldown = 0;
    public EnemyNormalState(Unit unit) : base(unit) {
        this.unit = unit as EnemyUnit;
    }

    protected override void UpdateState() {
        base.UpdateState();
        if (unit.closestEnemy == null) { return; }

        currentChillCooldown += Time.deltaTime;
        if (currentChillCooldown > chillCooldown) {
            unit.Chill();
        }
        if (Math.Abs(unit.closestDistance - unit.closestEnemy.weapon.MaxEffectiveRange) < 1 && unit.transform.TargetVisibility(unit.closestEnemy.transform.position, "Squader")) {
            //in range of the closest enemy && visible
            if (unit.Morale < 30) {
                unit.ThrowGrenade(unit.closestEnemy.transform.position);
            }
            if (unit.weapon.MaxAmmo/5 <= unit.CurrentAmmo) {
                
                Exit(new EnemyAttackState(unit, unit.closestEnemy));
            }
            else {
                unit.SetDestinationToSafety();
                Exit();
            }
            //attack state/reload state/go for objective
        }
        else {
            if (unit.weapon.MaxAmmo != unit.CurrentAmmo) {
                unit.CurrentState.ForceChangeState(new ReloadUnitState(unit, this));
            }
            if (unit.Morale > 70) {
                Exit(new EnemyAttackState(unit, unit.closestEnemy));
            }
        }
    }
}
