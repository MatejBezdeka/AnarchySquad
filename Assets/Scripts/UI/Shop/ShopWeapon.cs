using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Weapon")]
public class ShopWeapon : ShopItemBase {
    [SerializeField] Weapon weapon;
    public Weapon Weapon => weapon;
    protected override string GetItemName() {
        return weapon.name;
    }

    public override Sprite GetSprite() {
        return weapon.Icon;
    }
}
