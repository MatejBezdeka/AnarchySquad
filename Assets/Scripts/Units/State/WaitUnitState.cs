using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUnitState : UnitState
{
    protected WaitUnitState(SquadUnit unit) : base(unit) {
    }
    protected override void Enter() {
        base.Enter();
    }

    protected override void UpdateState() {
    }

    protected override void Exit(UnitState state) {
        base.Exit(state);
    }
}
