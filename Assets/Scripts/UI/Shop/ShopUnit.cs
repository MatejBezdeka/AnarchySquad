using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Unit")]
public class ShopUnit : ShopItemBase {
    [SerializeField] Stats stats;
    //ability
    //ability2
    public Stats Stats => stats;
    protected override string GetItemName() {
        Debug.Log(stats.UnitClass);
        return stats.UnitClass.ToString();
    }
}
