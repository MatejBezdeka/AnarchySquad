using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackupState : UnitState
{
    public EnemyBackupState(Unit unit) : base(unit) {
    }

    protected override void UpdateState() {
        Debug.Log("E backup");
    }
}
