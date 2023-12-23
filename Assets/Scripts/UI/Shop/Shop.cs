using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour {
    [Header("Description Text")]
    [SerializeField] GameObject description;
    [SerializeField] TextMeshProUGUI descriptionText;
    [Header("Shop")] 
    [SerializeField] GameObject itemPrefab;
    [SerializeField] ShopUnit[] units = new ShopUnit[15];
    [SerializeField] GameObject unitsShopGameObject;
    [SerializeField] ShopWeapon[] weapon = new ShopWeapon[15];
    [SerializeField] GameObject weaponsShopGameObject;
    [SerializeField] ShopItem[] items = new ShopItem[15];
    [SerializeField] GameObject itemsShopGameObject;

    void Start() {
        //Generate items
    }
}
