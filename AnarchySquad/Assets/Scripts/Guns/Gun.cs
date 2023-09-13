using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : ScriptableObject {
    [SerializeField, Range(1,100)] protected int damage;
    //[SerializeField, Range(1,100)] protected int armorPenetration;
    [SerializeField, Range(1,100)] protected float accuracy;
    [SerializeField, Range(1,100)] protected float maxEffectiveRange;
    //[SerializeField, Range(1,100)] protected float minEffectiveRange;
    [SerializeField, Range(1,100)] protected float attackSpeed;
    //[SerializeField, Range(1, 100)] protected int burstSize = 1; 
    //[SerializeField, Range(1,100)] protected int magSize;
    //[SerializeField, Range(1,100)] protected float reloadTime;
    //[SerializeField] protected Sprite icon;

    protected virtual void Shoot(Vector3 direction, float userAccuracy) {
        
    }
    protected virtual void Reload() {
        
    }
}
