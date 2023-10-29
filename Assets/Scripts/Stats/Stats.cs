using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Units/Stats")]
public class Stats : ScriptableObject {
    [SerializeField] string unitName = "Jeff";
    [SerializeField, Range(1, 100)] int maxHp;
    int hp;
    [SerializeField, Range(1,100)] float armor;
    [SerializeField, Range(0.1f,5)] float speed;
    [SerializeField, Range(20, 200)] float maxStamina;
    float stamina;
    [SerializeField, Range(1,100)] float range;
    [SerializeField, Range(1,100)] float accuracy;
    [SerializeField, Range(1, 100)] float maxEffectiveRange;
    [SerializeField] protected Sprite icon;
    Unit[] enemiesInRange;

    #region getters
    public string UnitName => unitName;
    public int MaxHp => maxHp;
    public int Hp => hp;
    public float Armor => armor;
    public float Speed => speed;
    public float Stamina => stamina;
    public float Range => range;
    public float Accuracy => accuracy;
    public float MaxEffectiveRange => maxEffectiveRange;
    public Sprite Icon => icon;
    #endregion

    //protected Inventory inventory
    public bool CalculateDamage(int damage) {
        damage = (int)(damage * (1f - (armor * 0.25f) * 0.04f));
        return (hp -= damage) <= 0;
    }

    public float Sprint() {
        //remove stamina
        return stamina;
    }

    public float Rest() {
        //add stamina
        return stamina;
    }
    public void Start() {
        hp = MaxHp;
    }
}
