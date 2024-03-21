using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunUnitState : UnitState {
    float tickCooldown = 1;
    float currentCooldown = 0;
    float speedMultiplayer = 2;
    float originalSpeed;
    public RunUnitState(Unit unit) : base(unit) {
    }
    protected override void Enter() {
        base.Enter();
        originalSpeed = unit.Agent.speed;
        unit.Agent.speed *= speedMultiplayer;
    }

    protected override void UpdateState() {
        currentCooldown += Time.deltaTime;
        if (currentCooldown > tickCooldown) {
            //Debug.Log(Math.Abs(unit.Agent.velocity.x) + Math.Abs(unit.Agent.velocity.z)  + " " + (unit.Agent.velocity.x + unit.Agent.velocity.z < 0.01f));
            if (Math.Abs(unit.Agent.velocity.x) + Math.Abs(unit.Agent.velocity.z) < 0.01f) {
                unit.AddStamina();
            }else if (unit.CurrentStamina== 0) {
                unit.Sprint();
                Exit(new NormalUnitState(unit));
            }
            currentCooldown = 0;
        }
    }

    protected override void Exit(UnitState state) {
        unit.Agent.speed = originalSpeed;
        base.Exit(state);
    }
}
