using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponState : UnitState {
    float currentCooldown = 0;
    float cooldown = 3;
    bool switched = false;
    SquadUnit unit;
    public SwitchWeaponState(Unit unit) : base(unit) {
        this.unit = unit as SquadUnit;
    }
    protected override void Enter() {
        Debug.Log("switching");
        unit.InvokeStartSwitching(cooldown);
        base.Enter();
    }
    protected override void UpdateState() {
        currentCooldown += Time.deltaTime;
        if (unit.selected) {
            unit.InvokeSwitching(cooldown-currentCooldown);
        }
        if (currentCooldown > cooldown) {
            switched = true;
            Exit(new NormalUnitState(unit));
        }
    }
    protected override void Exit(UnitState state) {
        if (switched) {
            unit.SwapWeapons();
        }
        unit.InvokeSwitching(-1);
        base.Exit(state);
    }
}