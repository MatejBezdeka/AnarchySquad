using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : UnitState
{
    System.Random rn = new System.Random();
    Unit target;
    EnemyUnit unit;
    protected bool attacking = false;
    protected float currentCooldown = 0;
    protected int currentBurst = 0;
    bool repeat = true;
    bool conditions;
    public EnemyAttackState(Unit unit, Unit target) : base(unit) {
        this.unit = unit as EnemyUnit;
        this.target = target;
        //low morale -> grenade/fight and slowly/(quickly)reatreat if no ammo
        //med -> stand your ground while you have ammo than you might have to cover
        //high -> stand ground/push back up a bit when reloading ot low ammo
    }

    protected override void Enter() {
        unit.needToReload += Reload;
        base.Enter();
    }

    protected override void UpdateState() {
        target = unit.closestEnemy;
        if (CheckConditions()) {
            float distance = Vector3.Distance(unit.transform.position, target.transform.position);
            Vector3 v = target.transform.position - unit.transform.position;
            v.y = 0;
            unit.transform.rotation = Quaternion.LookRotation(v);
            currentCooldown += Time.deltaTime;
            if (distance <= this.unit.MaxGrenadeDistance && rn.Next(500) == 250 && unit.currentGrenadeCooldown < 0) {
                this.unit.ThrowGrenade(target.transform.position);
            }

            if (distance < this.unit.weapon.EffectiveRange/2) {
                unit.Agent.SetDestination((unit.transform.position - target.transform.position).normalized * (unit.Agent.speed * Time.deltaTime));
            }
            else {
                unit.Agent.ResetPath();
            }
            unit.weapon.UpdateWeapon(unit, target, ref attacking,ref currentBurst, ref currentCooldown);
        }
    }

    protected override void Exit(UnitState state) {
        unit.Agent.ResetPath();
        repeat = false;
        base.Exit(state);
    }
    
    bool CheckConditions() {
        if (target == null || unit.Morale < 40) {
            Exit(new EnemyNormalState(unit));
        }
        float targetDistance = Vector3.Distance(unit.transform.position, target.transform.position);
        if (!unit.transform.TargetVisibility(target.transform.position, "Squader") ||
            targetDistance > unit.weapon.MaxEffectiveRange) {
            unit.Agent.SetDestination(target.transform.position);
            currentCooldown /= 2;
            return false;
        }
        if (targetDistance < unit.weapon.EffectiveRange/2) {
            unit.SetDestinationToSaferPlace();
            return true;
        }
        return true;
    }
    void Reload(float reloadTime) {
        Exit(new EnemyReloadUnitState(unit, this));
    }
}
