using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquadUnit : Unit {
    public event Action updateUI;
    UnitState currentState;

    protected override void Start() {
        base.Start();
        currentState = new NormalUnitState(this);
    }
    public void SetTarget(Unit target) {
        currentState.ForceChangeState(new AttackUnitState(this, agent,target));
        //weapon.LockOn(target.gameObject, , this, agent);
        //checkVisibilty
        //checkDistance
        //shoot
    }

    protected override void GetHit(int damage) {
        base.GetHit(damage);
        
    }

    void Update() {
        currentState = currentState.Process();
        updateUI?.Invoke();
    }
}
