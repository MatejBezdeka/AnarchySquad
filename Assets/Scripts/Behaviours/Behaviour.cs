using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Behaviour : ScriptableObject {
    // [SerializeField] protected bool friendly;
    [SerializeField] protected string unitName = "Jeff";
    [SerializeField, Range(1, 100)] protected int hp;
    [SerializeField, Range(1,100)] protected float armor;
    [SerializeField, Range(0.1f,5)] protected float speed;
    [SerializeField, Range(1,100)] protected float range;
    [SerializeField, Range(1,100)] protected float accuracy;
    [SerializeField, Range(1, 100)] public float maxEffectiveRange;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected Sprite model;
    
    Unit[] enemiesInRange;

    #region getters
    // public bool Friendly => friendly;
    public string UnitName => unitName;
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
    public virtual void Shoot(Unit target, Vector3 targetMoveVector, float distance) {
        //acuracy
    }

    public virtual void Move() {
        
    }
    
}
