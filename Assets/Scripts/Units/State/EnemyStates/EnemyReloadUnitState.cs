using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReloadUnitState : UnitState {
    float reloadTime;
    float currentCooldown = 0;
    bool reloaded = false;
    EnemyUnit unit;
    UnitState nextState;
    public EnemyReloadUnitState(Unit unit, UnitState nextState) : base(unit) {
        this.reloadTime = unit.weapon.ReloadTime;
        this.nextState = nextState;
        this.unit = unit as EnemyUnit;
        this.unit.SetDestinationToSafety();
    }
    public EnemyReloadUnitState(Unit unit) : base(unit) {
        this.reloadTime = unit.weapon.ReloadTime;
        nextState = new EnemyNormalState(unit);
    }
    protected override void Enter() {
        base.Enter();
    }
    protected override void UpdateState() {
        currentCooldown += Time.deltaTime;
        Debug.Log((int) currentCooldown + " " + reloadTime);
        if (currentCooldown > reloadTime) {
            this.unit.Reloaded();
            reloaded = true;
            Exit(new EnemyNormalState(unit));
        }
    }
    protected override void Exit(UnitState state) {
        currentCooldown = 0;
        this.unit.Agent.ResetPath();
        Debug.Log(unit.gameObject + " Finished");
        base.Exit(state);
    }
}