using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

public class UnitFactory : MonoBehaviour {
    public static Unit SpawnUnit(GameObject prefab, Stats newStats, Weapon weapon, Weapon secondary, Vector3 position) {
        GameObject newUnit = Instantiate(prefab, position, Quaternion.identity);
        SquadUnit comp = newUnit.GetComponent<SquadUnit>();
        comp.stats = newStats;
        comp.weapon = weapon;
        comp.secondaryWeapon = secondary;
        return comp;
    }
    public static SquadUnit SpawnUnit(GameObject prefab, UnitBlueprint blueprint, Vector3 position) {
        GameObject newUnit = Instantiate(prefab, position, Quaternion.identity);
        SquadUnit comp = newUnit.GetComponent<SquadUnit>();
        comp.UnitName = blueprint.name;
        comp.stats = blueprint.stats;
        if (blueprint.useSecAsMain) {
            comp.weapon = blueprint.secondaryWeapon;
        }
        else {
            comp.weapon = blueprint.weapon;
            if (blueprint.secondaryWeapon) {
                comp.secondaryWeapon = blueprint.secondaryWeapon;
            }
        }
        return comp;
    }
    public static EnemyUnit SpawnEnemy(GameObject prefab, UnitBlueprint blueprint, Vector3 position) {
        GameObject newUnit = Instantiate(prefab, position, Quaternion.identity);
        EnemyUnit comp = newUnit.GetComponent<EnemyUnit>();
        comp.UnitName = blueprint.name;
        comp.stats = blueprint.stats;
        comp.weapon = blueprint.weapon;
        return comp;
    }
}
