using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : AttackUnitState
{
    public EnemyAttackState(Unit unit, Unit target) : base(unit, target) {
        //low morale -> grenade/fight and slowly/(quickly)reatreat if no ammo
        //med -> stand your ground while you have ammo than you might have to cover
        //high -> stand ground/push back up a bit when reloading ot low ammo
    }
}
