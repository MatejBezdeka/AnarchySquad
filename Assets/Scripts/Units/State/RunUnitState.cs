using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunUnitState : UnitState {
    float tickCooldown = 1;
    float currentCooldown = 0;
    float speedMultiplayer = 2;
    public RunUnitState(SquadUnit unit) : base(unit) {
    }
    protected override void Enter() {
        base.Enter();
        unit.Agent.speed *= speedMultiplayer;
    }

    protected override void UpdateState() {
        currentCooldown += Time.deltaTime;
        if (currentCooldown > tickCooldown) {
            if (unit.CurrentSpeed < 0.05f) {
                unit.stats.AddStamina();
            }else if (unit.stats.Sprint() == 0) {
                Exit(new NormalUnitState(unit));
            }
            currentCooldown = 0;
        }
    }

    protected override void Exit(UnitState state) {
        unit.Agent.speed /= speedMultiplayer;
        base.Exit(state);
    }
}
