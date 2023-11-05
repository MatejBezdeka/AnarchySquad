using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadUnitState : UnitState {
    float reloadTime;
    float currentCooldown = 0;
    bool reloaded = false;
    UnitState previousState;
    public ReloadUnitState(SquadUnit unit, float reloadTime, UnitState previousState) : base(unit) {
        this.reloadTime = reloadTime;
        this.previousState = previousState;
    }
    public ReloadUnitState(SquadUnit unit, float reloadTime) : base(unit) {
        this.reloadTime = reloadTime;
        previousState = new NormalUnitState(unit);
    }
    protected override void Enter() {
        unit.InvokeReloading(reloadTime);
        base.Enter();
    }

    protected override void UpdateState() {
        currentCooldown += Time.deltaTime;
        if (currentCooldown > reloadTime) {
            unit.weapon.Reloaded();
            reloaded = true;
            Exit(previousState);
        }
    }

    protected override void Exit(UnitState state) {
        if (state.GetType() == typeof(AttackUnitState) && !reloaded) {
            currentStage = stateStages.update;
            return;
        }
        currentCooldown = 0;
        unit.InvokeReloading(0);
        base.Exit(state);
    }
}
