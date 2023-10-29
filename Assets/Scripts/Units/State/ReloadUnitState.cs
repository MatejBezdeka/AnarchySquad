using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadUnitState : UnitState
{
    protected ReloadUnitState(SquadUnit unit) : base(unit) {
    }
    protected override void Enter() {
        base.Enter();
    }

    protected override void UpdateState() {
        base.UpdateState();
    }

    protected override void Exit(UnitState state) {
        base.Exit(state);
    }
}
