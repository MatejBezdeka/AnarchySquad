using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Units/Stats")]
public class Stats : ShopItem {
    [Header("stats")]
    [SerializeField, Range(1, 100)] int maxHp;
    [SerializeField, Range(1,100)] float armor;
    [SerializeField, Range(0.1f,5)] float speed;
    [SerializeField, Range(20, 200)] float maxStamina;
    [SerializeField, Range(1,100)] float range;
    [SerializeField, Range(1,10)] float accuracy;
    [SerializeField, Range(1, 100)] float maxEffectiveRange;
    [SerializeField] protected Sprite icon;
    Unit[] enemiesInRange;

    #region getters
    public int MaxHp => maxHp;
    public float Armor => armor;
    public float Speed => speed;
    public float MaxStamina => maxStamina;
    public float Range => range;
    public float Accuracy => accuracy;
    public float MaxEffectiveRange => maxEffectiveRange;
    public Sprite Icon => icon;
    #endregion

    public override Shop.types GetType() {
        return Shop.types.unit;
    }

    public override Sprite GetSprite() {
        return icon;
    }
}
