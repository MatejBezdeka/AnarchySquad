using System;
using System.Collections;
using System.Collections.Generic;
using SceneBridges;
using TMPro;
using UI;
using UI.Shop;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    int points = 100;
    int selectedId;
    types selectedItemType;
    public static event Action<SquadUnit> addNewUnit; 
    public enum types {
        none,unit,weapon,item
    }
    [Header("Description Text")]
    [SerializeField] GameObject description;
    [SerializeField] TextMeshProUGUI descriptionText;
    [Header("Points Slider")] 
    [SerializeField] Slider pointsSlider;
    [SerializeField] TextMeshProUGUI pointsText;
    [Header("Shop")] 
    [SerializeField] GameObject itemPrefab;
    [SerializeField] List<ShopUnit> units;
    [SerializeField] GameObject unitsShopGameObject;
    [SerializeField] List<ShopWeapon> weapons;
    [SerializeField] GameObject weaponsShopGameObject;
    [SerializeField] List<ShopItem> items;
    [SerializeField] GameObject itemsShopGameObject;
    [Header("Team Container")]
    [SerializeField] int maxUnitsCount = 6;
    [SerializeField] GameObject containerGameObject;
    [SerializeField] GameObject memberPrefab;
    [SerializeField] TextMeshProUGUI unitCounter;
    [SerializeField] GameObject plusMemberPrefab; 
    List<SelectedTeamMemberContainer> containers = new List<SelectedTeamMemberContainer>();
    
    void Start() {
        ShopButton.itemClicked += ClickedItem;
        ShopButton.showDescription += ShowDescription;
        ShopButton.hideDescription += HideDescription;
        ShopButton.deselected += Deselect;
        IUnitButton.clickedUnitButton += ClickedUnit;
        StartMissionButton.startingGame += SaveUnitsForNextScene;
        InstantiateList(unitsShopGameObject, new List<ShopItemBase>(units));
        InstantiateList(weaponsShopGameObject, new List<ShopItemBase>(weapons));
        InstantiateList(itemsShopGameObject, new List<ShopItemBase>(items));
        SelectedTeamMemberContainer.RemoveUnit += RemoveUnit;
        plusMemberPrefab = Instantiate(plusMemberPrefab, containerGameObject.transform);
    }
    void InstantiateList(GameObject body,List<ShopItemBase> list) {
        for (int i = 0; i < list.Count; i++) {
            var comp = Instantiate(itemPrefab).GetComponent<ShopButton>();
            comp.transform.parent = body.transform;
            comp.type = GetEnumFromType(list[0].GetType());
            comp.id = i;
            comp.SetText(list[i].itemName + "\n" + list[i].Cost + " PT");
        }
    }

    void ClickedUnit(Tuple<SquadUnit, int> obj) {
        //plus button
        if (obj.Item2 == -1) {
            AddUnit();
            //add new unit with either stats/weapon or item in invenotry 
            //addStats.Invoke(selectedStats);
            return;
        }
        switch (selectedItemType) { 
            case types.unit:
                var newStats = units[selectedId].Stats;
                newStats.unitName = Names.GetRandomName();
                containers[obj.Item2].SetStats(newStats);
                break;
            case types.weapon:
                var weapon = weapons[selectedId].Weapon;
                break;
            case types.item:
                //TODO:
                break;
            default:
                break;
        }
        
        //add or overwrite items on unit
    }

    void ClickedItem(Tuple<types, int> identification) {
        selectedItemType = identification.Item1;
        selectedId = identification.Item2;
        //subtract points
    }
    void ShowDescription(Tuple<types, int> identification) {
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
    void RemoveUnit(SquadUnit unit) {
        foreach (var comp in containers) {
            if (comp.Unit == unit) {
                containers.Remove(comp);
                UpdateCount();
                return;
            }
        }
        
    }
    void AddUnit() {
        if (containers.Count == maxUnitsCount) {
            return;
        }
        GameObject container = Instantiate(memberPrefab, containerGameObject.transform);
        var comp = container.GetComponent<SelectedTeamMemberContainer>();
        comp.SetId(containers.Count);
        containers.Add(comp);
        UpdateCount();
    }
    void UpdateCount() {
        unitCounter.text = containers.Count + "/" + maxUnitsCount;
        plusMemberPrefab.SetActive(true);
        plusMemberPrefab.transform.SetSiblingIndex(transform.childCount);
        if (containers.Count == maxUnitsCount) {
            plusMemberPrefab.SetActive(false);
        }
    }
    void HideDescription() {
        description.SetActive(false);
    }

    void Deselect() {
        Debug.Log("unselected");
        selectedId = -1;
        selectedItemType = types.none;
    }
    void SaveUnitsForNextScene() {
        foreach (var comp in containers) {
            SquadParameters.Units.Add(comp.Unit);    
        }
    }
}
