using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour {
    public static event Action<Stats> addStats; 
    public enum types {
        unit,weapon,item
    }
    [Header("Description Text")]
    [SerializeField] GameObject description;
    [SerializeField] TextMeshProUGUI descriptionText;
    [Header("Shop")] 
    [SerializeField] GameObject itemPrefab;
    [SerializeField] List<ShopUnit> units;
    [SerializeField] GameObject unitsShopGameObject;
    [SerializeField] List<ShopWeapon> weapons;
    [SerializeField] GameObject weaponsShopGameObject;
    [SerializeField] List<ShopItem> items;
    [SerializeField] GameObject itemsShopGameObject;
    void Start() {
        ShopButton.itemClicked += ClickedItem;
        ShopButton.showDescription += ShowDescription;
        ShopButton.hideDescription += HideDescription;
        InstantiateList(unitsShopGameObject, new List<ShopItemBase>(units));
        InstantiateList(weaponsShopGameObject, new List<ShopItemBase>(weapons));
        InstantiateList(itemsShopGameObject, new List<ShopItemBase>(items));
    }

    void InstantiateList(GameObject body,List<ShopItemBase> list) {
        for (int i = 0; i < list.Count; i++) {
            var comp = Instantiate(itemPrefab).GetComponent<ShopButton>();
            comp.transform.parent = body.transform;
            comp.type = GetEnumFromType(list[0].GetType());
            comp.id = i;
        }
    }
    

    void ClickedItem(Tuple<types, int> identification) {
        switch (identification.Item1) {
            case types.unit:
                Debug.Log("unit");
                //units[identification.Item2];
                addStats.Invoke(units[identification.Item2].Stats);
                break;
            case types.weapon:
                Debug.Log("wp");
                
                break;
            case types.item:
                Debug.Log("it");
                break;
        }
    }void ShowDescription(Tuple<types, int> identification) {
        description.SetActive(true);
        switch (identification.Item1) {
            case types.unit:
                descriptionText.text = units[identification.Item2].Description;
                break;
            case types.weapon:
                descriptionText.text = weapons[identification.Item2].Description;
                break;
            case types.item:
                descriptionText.text = items[identification.Item2].Description;
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

    void HideDescription() {
        description.SetActive(false);
    }
}
