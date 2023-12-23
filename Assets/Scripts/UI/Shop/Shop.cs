using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;

public class Shop : MonoBehaviour {
    public enum types {
        unit,weapon,item
    }
    [Header("Description Text")]
    [SerializeField] GameObject description;
    [SerializeField] TextMeshProUGUI descriptionText;
    [Header("Shop")] 
    [SerializeField] GameObject itemPrefab;
    [SerializeField] ShopUnit[] units = new ShopUnit[1];
    [SerializeField] GameObject unitsShopGameObject;
    [SerializeField] ShopWeapon[] weapons = new ShopWeapon[1];
    [SerializeField] GameObject weaponsShopGameObject;
    [SerializeField] ShopItem[] items = new ShopItem[1];
    [SerializeField] GameObject itemsShopGameObject;
    void Start() {
        ShopButton.itemClicked += ClickedItem;
        InstantiateList(unitsShopGameObject, units);
        InstantiateList(weaponsShopGameObject, weapons);
        InstantiateList(itemsShopGameObject, items);
    }

    void InstantiateList(GameObject body,Array list) {
        for (int i = 0; i < list.Length; i++) {
            var comp = Instantiate(itemsShopGameObject).GetComponent<ShopButton>();
            comp.type = GetEnumFromType(list.GetValue(0).GetType());
            comp.id = i;
        }
    }
    

    void ClickedItem(Tuple<types, int> identification) {
        switch (identification.Item1) {
            case types.unit:
                Debug.Log("unit");
                var a = units[identification.Item2];
                break;
            case types.weapon:
                Debug.Log("wp");
                break;
            case types.item:
                Debug.Log("it");
                break;
        }
    }

    types GetEnumFromType(Type item) {
        if (item == typeof(ShopUnit)) {
            return types.unit;
        }
        if (item == typeof(ShopWeapon)) {
            return types.weapon;
        }
        if (item == typeof(ShopItem)) {
            return types.item;
        }

        return types.unit;
    }
}
