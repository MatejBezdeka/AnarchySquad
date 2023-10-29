using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour
{
    public enum stateStages {
        entry, update, exit
    }

    public enum state {
        normal, attack, reload, run, wait
    }
    stateStages currentStage = stateStages.entry;
    UnitState nextState;
    SquadUnit unit;
    public UnitState(SquadUnit unit) {
        this.unit = unit;
    }
    protected virtual void Enter() {
        currentStage = stateStages.update;
    }
    protected virtual void UpdateState() { }

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
