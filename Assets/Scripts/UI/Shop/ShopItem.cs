using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ShopItem : ScriptableObject {
    [Header("Shop item stats")]
    [SerializeField] protected int cost;
    public string itemName => GetItemName();
    [SerializeField,TextArea(3,10)] protected string description;
    public string Description => description;
    public int Cost => cost;
    public string GetItemName() {
        return name;
    }
    public abstract Shop.types GetType();
    public abstract Sprite GetSprite();
}
