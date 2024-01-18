using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitState
{
    protected enum stateStages {
        entry, update, exit
    }
    protected stateStages currentStage = stateStages.entry;
    UnitState nextState;
    protected SquadUnit unit;

    protected UnitState(SquadUnit unit) {
        this.unit = unit;
    }

    protected virtual void Enter() {
        currentStage = stateStages.update;
    }
    
    protected abstract void UpdateState();
    
    protected virtual void Exit(UnitState state) {
        nextState = state;
        currentStage = stateStages.exit;
    }
    public UnitState Process() {
        switch (currentStage) {
            case stateStages.entry:
                Enter();
                break;
            case stateStages.update:
                UpdateState();
                break;
            case stateStages.exit:
            default:
                currentStage = stateStages.exit;
                return nextState;
        }
        return this;
    }
    public void ForceChangeState(UnitState newState) {
        Exit(newState);
    }
}