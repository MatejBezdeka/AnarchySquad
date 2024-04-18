using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackupState : UnitState {
    EnemyUnit unit;
    public EnemyBackupState(Unit unit) : base(unit) {
        this.unit = unit as EnemyUnit;
    }

    protected override void UpdateState() {
        unit.RegenMorale();
    }
}
