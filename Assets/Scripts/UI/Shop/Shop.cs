using System;
using System.Collections;
using System.Collections.Generic;
using SceneBridges;
using TMPro;
using UI;
using Units;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    int points = 1000;
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
    [Header("Error message")]
    [SerializeField] ErrorMessage errorMessageManager;
    [Header("Shop")] 
    [SerializeField] GameObject itemPrefab;
    [SerializeField] List<Stats> unitStats;
    [SerializeField] GameObject unitsShopGameObject;
    [SerializeField] List<Weapon> weapons;
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
        UnitButton.clickedUnitButton += ClickedUnit;
        StartMissionButton.startingGame += SaveUnitsForNextScene;
        InstantiateList(unitsShopGameObject, new List<ShopItem>(unitStats));
        InstantiateList(weaponsShopGameObject, new List<ShopItem>(weapons));
        //InstantiateList(itemsShopGameObject, new List<ShopItemBase>(items));
        SelectedTeamMemberContainer.RemoveUnit += RemoveUnit;
        SelectedTeamMemberContainer.RemoveElement += RemoveElement;
        SelectedTeamMemberContainer.hideDescription += HideDescription;
        SelectedTeamMemberContainer.showUnitDescription += ShowUnitDescription;
        plusMemberPrefab = Instantiate(plusMemberPrefab, containerGameObject.transform);
        pointsSlider.maxValue = points;
        UpdateSlider();
        AudioSettings.Music.ChangeAmbientMusic(AudioSettings.AmbientMusic.Hub);
    }

    void UpdateSlider() {
        pointsSlider.value = points;
        pointsText.text = points + "/" + pointsSlider.maxValue;
    }

    void InstantiateList(GameObject body,List<ShopItem> list) {
        for (int i = 0; i < list.Count; i++) {
            var comp = Instantiate(itemPrefab).GetComponent<ShopButton>();
            comp.transform.SetParent(body.transform);
            comp.type = list[0].GetType();
            comp.id = i;
            comp.SetGraphics(list[i].itemName, list[i].Cost, list[i].GetSprite());
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

        int cost = 0;
        switch (obj.Item2) { 
            case types.unit:
                var newStats = unitStats[selectedId];
                cost = newStats.Cost;
                if (cost - (unitBlueprints[id].stats ? unitBlueprints[id].stats.Cost : 0) > points) return;
                if (unitBlueprints[id].stats != null) RemoveElement(new Tuple<types, int>(types.unit, id)); 
                unitBlueprints[id].name = Names.GetRandomName();
                unitBlueprints[id].stats = newStats;
                containers[id].SetStats(unitBlueprints[id].name,newStats.Icon);
                break;
            case types.weapon:
                Weapon newWeapon = weapons[selectedId];
                
                cost = newWeapon.Cost;
                if (cost - (unitBlueprints[id].weapon ? unitBlueprints[id].weapon.Cost : 0) > points) return;
                if (unitBlueprints[id].weapon != null) RemoveElement(new Tuple<types, int>(types.weapon, id)); 
                unitBlueprints[id].weapon = newWeapon;
                containers[id].SetWeapon(newWeapon);
                break;
            case types.secondaryWeapon:
                var newSecWeapon = weapons[selectedId];
                cost = newSecWeapon.Cost;
                if (cost - (unitBlueprints[id].secondaryWeapon ? unitBlueprints[id].secondaryWeapon.Cost : 0) > points) return;
                if (unitBlueprints[id].secondaryWeapon != null) RemoveElement(new Tuple<types, int>(types.secondaryWeapon, id)); 
                unitBlueprints[id].secondaryWeapon = newSecWeapon;
                containers[id].SetSecondaryWeapon(newSecWeapon);
                break;
            case types.item:
                //TODO:
                break;
            default:
                cost = 0;
                break;
        }
        points -= cost;
        UpdateSlider();
        //add or overwrite items on unit
    }

    void ClickedItem(Tuple<types, int> identification) {
        selectedItemType = identification.Item1;
        selectedId = identification.Item2;
        //subtract points
    }
    void ShowDescription(Tuple<types, int> identification) {
        switch (identification.Item1) {
            case types.unit:
                descriptionText.text = unitStats[identification.Item2].GetDescription();
                //descriptionText.text = unitStats[identification.Item2].Description;
                break;
            case types.weapon:
                descriptionText.text = weapons[identification.Item2].GetDescription();
                //descriptionText.text = weapons[identification.Item2].Description;
                break;
            case types.item:
                descriptionText.text = items[identification.Item2].Description;
                break;
        }
    }

    void ShowUnitDescription(int id) {
        descriptionText.text = unitBlueprints[id].GetDescription();
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
    void RemoveElement(Tuple<types, int>obj) {
        var unit = unitBlueprints[obj.Item2];
        switch (obj.Item1) {
            case types.unit:
                points += unit.stats.Cost;
                unit.stats = null;
                break;
            case types.weapon:
                points += unit.weapon.Cost;
                unit.weapon = null;
                break;
            case types.secondaryWeapon:
                points += unit.secondaryWeapon.Cost;
                unit.secondaryWeapon = null;
                break;
        }
        UpdateSlider();
    }
    void RemoveUnit(int id) {
        points += unitBlueprints[id].GetCurrentValue();
        UpdateSlider();
        unitBlueprints.RemoveAt(id);
        containers.RemoveAt(id);
        UpdateCount();
        for (int i = 0; i < unitBlueprints.Count; i++) {
            containers[i].SetId(i);
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
        descriptionText.text = "";
        //description.SetActive(false);
    }

    void Deselect() {
        Debug.Log("unselected");
        selectedId = -1;
        selectedItemType = types.none;
    }
    void SaveUnitsForNextScene() {
        if (unitBlueprints.Count == 0) {
            errorMessageManager.SetErrorMessage("You need at least 1 unit!");
            return;
        }
        foreach (var comp in unitBlueprints) {
            if (!comp.IsValid()) {
                errorMessageManager.SetErrorMessage("Every unit needs class and at least 1 weapon!");
                return;
            }
        }
        foreach (var comp in unitBlueprints) {
            SquadParameters.Units.Add(comp);    
        }

        SceneManager.LoadSceneAsync("LoadingScene");
        SceneManager.LoadSceneAsync("Battlefield");
    }

    public void RemoveListeners() {
        ShopButton.itemClicked -= ClickedItem;
        ShopButton.showDescription -= ShowDescription;
        ShopButton.hideDescription -= HideDescription;
        ShopButton.deselected -= Deselect;
        UnitButton.clickedUnitButton -= ClickedUnit;
        StartMissionButton.startingGame -= SaveUnitsForNextScene;
        SelectedTeamMemberContainer.RemoveUnit -= RemoveUnit;
        SelectedTeamMemberContainer.RemoveElement -= RemoveElement;
        SelectedTeamMemberContainer.hideDescription -= HideDescription;
        SelectedTeamMemberContainer.showUnitDescription -= ShowUnitDescription;
    }
}
