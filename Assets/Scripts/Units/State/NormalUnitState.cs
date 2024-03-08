using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalUnitState : UnitState {
    float restCooldown = 2;
    float currentCooldown = 0;
    public SquadUnit bel;
    public NormalUnitState(Unit unit) : base(unit) {
    }

    protected override void Enter() {
        base.Enter();
    }

    protected override void UpdateState() {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= restCooldown) {
            unit.AddStamina();
            currentCooldown = 0;
        }
    }

    protected override void Exit(UnitState state) {
        base.Exit(state);
    }
}
