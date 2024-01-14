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
        unit.InvokeStartReloading(reloadTime);
        base.Enter();
    }
    protected override void UpdateState() {
        currentCooldown += Time.deltaTime;
        unit.InvokeReloading(reloadTime-currentCooldown);
        if (currentCooldown > reloadTime) {
            unit.weapon.Reloaded();
            reloaded = true;
            Exit(previousState);
        }
    }
    protected override void Exit(UnitState state) {
        currentCooldown = 0;
        unit.InvokeReloading(-1);
        //prevents repeating reload while reloading
        if (state is AttackUnitState && !reloaded || state is ReloadUnitState) {
            currentStage = stateStages.update;
            return;
        }
        base.Exit(state);
    }
}
