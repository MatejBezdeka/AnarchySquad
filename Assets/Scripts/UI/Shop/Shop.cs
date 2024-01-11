using System;
using System.Collections;
using System.Collections.Generic;
using SceneBridges;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    int points = 100;
    int selectedId;
    types selectedItemType;
    public static event Action<SquadUnit> addNewUnit; 
    public enum types {
        none,unit,weapon,secondaryWeapon,item
    }
    [Header("Description Text")]
    [SerializeField] GameObject description;
    [SerializeField] TextMeshProUGUI descriptionText;
    [Header("Points Slider")] 
    [SerializeField] Slider pointsSlider;
    [SerializeField] TextMeshProUGUI pointsText;
    [Header("Shop")] 
    [SerializeField] GameObject itemPrefab;
    [SerializeField] List<ShopUnit> unitStats;
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
    List<UnitBlueprint> unitBlueprints = new List<UnitBlueprint>();

    void Start() {
        ShopButton.itemClicked += ClickedItem;
        ShopButton.showDescription += ShowDescription;
        ShopButton.hideDescription += HideDescription;
        ShopButton.deselected += Deselect;
        IUnitButton.clickedUnitButton += ClickedUnit;
        StartMissionButton.startingGame += SaveUnitsForNextScene;
        InstantiateList(unitsShopGameObject, new List<ShopItemBase>(unitStats));
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

    void ClickedUnit(Tuple<int, types> obj) {
        //plus button
        int id = obj.Item1;
        if (id == -1) {
            AddUnit();
            return;
        }
        if (obj.Item2 != selectedItemType) {
            Debug.Log(obj.Item2 + "  : " + selectedItemType);
            if (obj.Item2 == types.secondaryWeapon && selectedItemType == types.weapon) {
                //ignore
            }
            else {
                return;
            }
        }
        switch (obj.Item2) { 
            case types.unit:
                var newStats = unitStats[selectedId].Stats;
                newStats.unitName = Names.GetRandomName();
                unitBlueprints[id].stats = newStats;
                containers[id].SetStats(newStats);
                break;
            case types.weapon:
                var newWeapon = weapons[selectedId].Weapon;
                unitBlueprints[id].weapon = newWeapon;
                containers[id].SetWeapon(newWeapon);
                break;
            case types.secondaryWeapon:
                var newSecWeapon = weapons[selectedId].Weapon;
                unitBlueprints[id].secondaryWeapon = newSecWeapon;
                containers[id].SetSecondaryWeapon(newSecWeapon);
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
                descriptionText.text = unitStats[identification.Item2].Description;
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
    /*void RemoveUnit(int id) {
        foreach (var comp in containers) {
            if (comp.Unit == unit) {
                containers.Remove(comp);
                UpdateCount();
                return;
            }
        }
    }*/
    void RemoveUnit(int id) {
        unitBlueprints.RemoveAt(id);
        containers.RemoveAt(id);
        UpdateCount();
    }
    void AddUnit() {
        if (containers.Count == maxUnitsCount) {
            return;
        }
        GameObject container = Instantiate(memberPrefab, containerGameObject.transform);
        var comp = container.GetComponent<SelectedTeamMemberContainer>();
        comp.SetId(containers.Count);
        containers.Add(comp);
        var newUnit = new UnitBlueprint();
        unitBlueprints.Add(newUnit);
        UpdateCount();
    }
    void UpdateCount() {
        unitCounter.text = unitBlueprints.Count + "/" + maxUnitsCount;
        plusMemberPrefab.SetActive(true);
        plusMemberPrefab.transform.SetSiblingIndex(transform.childCount);
        if (unitBlueprints.Count == maxUnitsCount) {
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
        foreach (var comp in unitBlueprints) {
            if (!comp.IsValid()) {
                return;
            }
        }
        foreach (var comp in unitBlueprints) {
            SquadParameters.Units.Add(comp);    
        }
    }
}
