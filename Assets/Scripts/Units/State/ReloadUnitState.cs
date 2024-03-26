using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadUnitState : UnitState {
    float reloadTime;
    float currentCooldown = 0;
    bool reloaded = false;
    UnitState nextState;
    public ReloadUnitState(Unit unit, UnitState nextState) : base(unit) {
        this.reloadTime = unit.weapon.ReloadTime;
        this.nextState = nextState;
    }
    public ReloadUnitState(Unit unit) : base(unit) {
        this.reloadTime = unit.weapon.ReloadTime;
        nextState = new NormalUnitState(unit);
    }
    protected override void Enter() {
        unit.InvokeStartReloading(reloadTime);
        base.Enter();
    }
    protected override void UpdateState() {
        currentCooldown += Time.deltaTime;
        if (unit.selected) {
            unit.InvokeReloading(reloadTime - currentCooldown);
        }
        if (currentCooldown > reloadTime) {
            unit.Reloaded();
            reloaded = true;
            Exit(nextState);
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
        unit.PlayAudioClip(Unit.AudioClips.reload);
        base.Exit(state);
    }
}