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
        unit.agent.speed *= speedMultiplayer;
    }

    protected override void UpdateState() {
        currentCooldown += Time.deltaTime;
        if (currentCooldown > tickCooldown) {
            if (unit.stats.Sprint() == 0) {
                Exit(new NormalUnitState(unit));
            }
        }
    }

    protected override void Exit(UnitState state) {
        unit.agent.speed /= speedMultiplayer;
        base.Exit(state);
    }
}
