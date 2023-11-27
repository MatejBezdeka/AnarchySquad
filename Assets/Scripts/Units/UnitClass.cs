﻿using System;
using UnityEngine;

namespace Units {
    [CreateAssetMenu(menuName = "Units/Class")]
    public class UnitClass : ScriptableObject {
        [SerializeField] Stats[] statsPool;
        [SerializeField] Weapon weapon;
        [SerializeField] Weapon secondaryWeapon;
        [SerializeField] SquadUnit unitsPool;
        [SerializeField] string className;
        enum Class {
            Scout, Assault, Heavy, Specialist
        }
    }
}