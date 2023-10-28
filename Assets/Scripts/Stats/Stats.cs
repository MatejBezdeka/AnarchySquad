using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Units/Stats")]
public class Stats : ScriptableObject {
    [SerializeField] protected string unitName = "Jeff";
    [SerializeField, Range(1, 100)] protected int maxHp;
    protected int hp;
    [SerializeField, Range(1,100)] protected float armor;
    [SerializeField, Range(0.1f,5)] protected float speed;
    [SerializeField, Range(20, 200)] protected float maxStamina;
    protected float stamina;
    [SerializeField, Range(1,100)] protected float range;
    [SerializeField, Range(1,100)] protected float accuracy;
    [SerializeField, Range(1, 100)] public float maxEffectiveRange;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected Sprite model;
    Unit[] enemiesInRange;

    #region getters
    public string UnitName => unitName;
    public int MaxHp => maxHp;
    public int Hp => hp;
    public float Armor => armor;
    public float Speed => speed;
    public float Range => range;
    public float Accuracy => accuracy;
    public float MaxEffectiveRange => maxEffectiveRange;
    public Sprite Icon => icon;
    public Sprite Model => model;
    #endregion

    //protected Inventory inventory
    public bool CalculateDamage(int damage) {
        damage = (int)(damage * (1f - (armor * 0.25f) * 0.04f));
        return (hp -= damage) <= 0;
    }

    public void Start() {
        hp = MaxHp;
    }
}
