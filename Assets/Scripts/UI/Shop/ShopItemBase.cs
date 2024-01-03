using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ShopItemBase : ScriptableObject {
    [SerializeField] protected int cost;
    [HideInInspector] public string itemName => GetItemName();
    [SerializeField,TextArea(3,10)] protected string description;
    public string Description => description;
    public int Cost => cost;

    protected abstract string GetItemName();
}
