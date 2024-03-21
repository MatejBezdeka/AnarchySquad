using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalState : NormalUnitState {
    EnemyUnit unit;
    public EnemyNormalState(Unit unit) : base(unit) {
        this.unit = unit as EnemyUnit;
    }

    protected override void UpdateState() {
        base.UpdateState();
        if (unit.closestEnemy == null) {
            return;
        }
        if (Math.Abs(unit.closestDistance - unit.closestEnemy.weapon.MaxEffectiveRange) < 1 && unit.transform.TargetVisibility(unit.closestEnemy.transform.position, "Squader")) {
            //in range of the closest enemy && visible
            if (unit.Morale < 30) {
                //desperate grenade
            }
            //attack state/reload state/go for objective
        }
        else {
            if (unit.weapon.MaxAmmo != unit.CurrentAmmo) {
                unit.CurrentState.ForceChangeState(new ReloadUnitState(unit, this));
            }
            if (unit.Morale < 70) {
                //desperate measure? (suicide)
                //chill();
                //HEAL?() [once per life]
            }
            else {
                //attack/go for objective
            }
            
        }
        
    }
}
