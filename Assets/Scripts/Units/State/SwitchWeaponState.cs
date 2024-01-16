using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponState : UnitState {
    float currentCooldown = 0;
    float cooldown = 3;
    public SwitchWeaponState(SquadUnit unit) : base(unit) {
    }
    protected override void Enter() {
    }
    protected override void UpdateState() {
        
    }
    protected override void Exit(UnitState state) {
        base.Exit(state);
    }
}
