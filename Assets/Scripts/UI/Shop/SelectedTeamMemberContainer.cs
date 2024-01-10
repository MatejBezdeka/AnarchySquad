using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Shop;
using Units;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTeamMemberContainer : IUnitButton {
    public static event Action<int> RemoveUnit;
    int id;
    UnitBlueprint unit = new UnitBlueprint();
    [SerializeField] Button removeButton;
    [SerializeField] TextMeshProUGUI unitName;
    [SerializeField] TextMeshProUGUI className;
    [SerializeField] Image unitImage;
    [SerializeField] Sprite placeHolderImg;
    [SerializeField] string placeHolderName;
    [SerializeField] string placeHolderClass;
    public UnitBlueprint Unit => unit;
    
    protected override void Start() {
        base.Start();
        removeButton.onClick.AddListener(RemoveButtonClicked);
        unitImage.sprite = placeHolderImg;
        unitName.text = placeHolderName;
        className.text = placeHolderClass;
    }

    protected override int GetId() {
        return id;
    }

    public void SetId(int newId) {
        id = newId;
    }
    protected override UnitBlueprint ReturnUnit() {
        return unit;
    }

    public void SetStatsGraphics(Stats stats) {
        unit.stats = stats;
        unitName.text = stats.UnitName;
        unitImage.sprite = stats.Icon;
        className.text = stats.UnitClass.ToString();
    }

    public void SetWeapon(Weapon weapon, bool secondary) {
        if (secondary) {
            unit.secondaryWeapon = weapon;
        }
        else {
            unit.weapon = weapon;
        }
        
    }

    /*public void SetUnit(SquadUnit unit, int id) {
        this.id = id;
        this.unit = unit;
        if (unit.stats) {
            unitName.text = unit.stats.UnitName;
            unitImage.sprite = unit.stats.Icon;
            className.text = unit.stats.UnitClass.ToString();
        }
    }*/
    //TODO:
    //public void SetItem()
    void RemoveButtonClicked() {
        RemoveUnit?.Invoke(id);
        Destroy(gameObject);
    }
}
