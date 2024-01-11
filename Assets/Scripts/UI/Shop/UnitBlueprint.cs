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
}
