using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShopItemBase : ScriptableObject {
    [SerializeField] protected int cost;
    [SerializeField] protected string className;
    [SerializeField,TextArea] protected string description;
    public string Description => description;
}
