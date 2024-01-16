using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

public class UnitBlueprint {
    public Stats stats;
    public Weapon weapon;
    public Weapon secondaryWeapon;
    public bool IsValid() {
        return stats != null && weapon != null;
    }

    public int GetCurrentValue() {
        int value = 0;
        if (stats != null) {
            value += stats.Cost;
        }

        if (weapon != null) {
            value += weapon.Cost;
        }

        if (secondaryWeapon != null) {
            value += secondaryWeapon.Cost;
        }

        return value;
    }
}
