using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AI : ScriptableObject {
    [SerializeField] protected bool friendly;
    [SerializeField] public string unitName = "Jeff";
    [SerializeField, Range(1,100)] protected int hp; 
    [SerializeField, Range(1,100)] protected int armor;
    [SerializeField, Range(1,100)] protected int speed;
    [SerializeField, Range(1,100)] protected int range;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected Sprite model;
    
    [SerializeField, Range(1,100)] protected float accuracy;
    [SerializeField, Range(1, 100)] public float maxEffectiveRange;
    
    //protected Inventory inventory
    protected virtual void Shoot() {
        
    }

    protected virtual void Move() {
        
    }
    void Die() {
        
    }
    
}
