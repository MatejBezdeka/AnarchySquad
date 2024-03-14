using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

public class UnitBlueprint {
    public string name;
    public Stats stats;
    public Weapon weapon;
    public Weapon secondaryWeapon;
    public bool useSecAsMain = false;
    
    public UnitBlueprint() { }
    
    public UnitBlueprint(string name, Stats stats, Weapon weapon) {
        this.name = name;
        this.stats = stats;
        this.weapon = weapon;
    }
    public UnitBlueprint(string name, Stats stats, Weapon weapon, Weapon secondaryWeapon) {
        this.name = name;
        this.stats = stats;
        this.weapon = weapon;
        this.secondaryWeapon = secondaryWeapon;
    }

    public bool IsValid() {
        if (weapon == null && secondaryWeapon != null) {
            useSecAsMain = true;
        }
        return stats != null && weapon != null || stats != null && secondaryWeapon != null;
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

    public string GetDescription() {
        string description = "";
        if (stats) {
            description += name + "\n" + stats.GetDescription();
        }

        if (weapon) {
            description += "\n" + weapon.GetDescription();
        }

        if (secondaryWeapon) {
            description += "\n" + secondaryWeapon.GetDescription();
        }
        return description;
    }
}
