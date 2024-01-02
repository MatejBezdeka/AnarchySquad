using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Unit")]
public class ShopUnit : ShopItemBase {
    
    [SerializeField] Stats stats;
    //ability
    //ability2
    public Stats Stats => stats;
    public static string GetRandomName() {
        return "random name";
    }
    
}
