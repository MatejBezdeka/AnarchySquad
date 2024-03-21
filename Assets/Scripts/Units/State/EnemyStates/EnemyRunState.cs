using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : RunUnitState
{
    public EnemyRunState(Unit unit) : base(unit) {
        //low morale (0-60) -> run away to safe block / safe distance best 100
        //
        //high morale -> if enemy is far away 150m+ run until 100meters?
    }
    
}
