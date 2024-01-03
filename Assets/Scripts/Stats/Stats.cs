using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Units/Stats")]
public class Stats : ScriptableObject {
    [HideInInspector] public string unitName = "No name";

    public enum unitClasses {
        Scout, Breacher
    }
    [SerializeField] private unitClasses unitClass;
    [SerializeField, Range(1, 100)] int maxHp;
    int currentHp;
    [SerializeField, Range(1,100)] float armor;
    [SerializeField, Range(0.1f,5)] float speed;
    [SerializeField, Range(20, 200)] float maxStamina;
    float currentStamina;
    [SerializeField, Range(1,100)] float range;
    [SerializeField, Range(1,10)] float accuracy;
    [SerializeField, Range(1, 100)] float maxEffectiveRange;
    [SerializeField] protected Sprite icon;
    Unit[] enemiesInRange;

    #region getters
    public unitClasses UnitClass => unitClass;
    public string UnitName => unitName;
    public int MaxHp => maxHp;
    public int CurrentHp => currentHp;
    public float Armor => armor;
    public float Speed => speed;
    public float MaxStamina => maxStamina;
    public float CurrentStamina => currentStamina;
    public float Range => range;
    public float Accuracy => accuracy;
    public float MaxEffectiveRange => maxEffectiveRange;
    public Sprite Icon => icon;
    #endregion
    //protected Inventory inventory
    public bool CalculateDamage(int damage) {
        damage = (int)(damage * (1f - (armor * 0.25f) * 0.04f));
        return (currentHp -= damage) <= 0;
    }

    public float Sprint() {
        currentStamina -= 2f;
        if (currentStamina < 0) {
            currentStamina = 0;
        }
        return currentStamina;
    }

    public float AddStamina() {
        currentStamina += 1.5f;
        if (currentStamina >= maxStamina) {
            currentStamina = maxStamina;
        }
        return currentStamina;
    }
    public void Start() {
        currentHp = MaxHp;
        currentStamina = maxStamina;
    }
}
