using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunUnitState : UnitState
{
    protected RunUnitState(SquadUnit unit) : base(unit) {
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
