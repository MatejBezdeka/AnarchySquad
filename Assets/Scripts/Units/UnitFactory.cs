using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

public class UnitFactory : MonoBehaviour {
    public SquadUnit SpawnUnit(GameObject prefab, Stats newStats, Weapon weapon, Weapon secondary, Vector3 position) {
        GameObject newUnit = Instantiate(prefab, position, Quaternion.identity);
        SquadUnit comp = newUnit.GetComponent<SquadUnit>();
        comp.stats = newStats;
        comp.weapon = weapon;
        comp.secondaryWeapon = secondary;
        comp.stats.Start();
        comp.weapon.Start();
        comp.secondaryWeapon.Start();
        return comp;
    }
}
