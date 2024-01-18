using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public static event Action grenadeAction;

    [Header("Profile")] 
    [SerializeField] Slider hpSlider;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Slider ammoSlider;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Slider staminaSlider;
    [SerializeField] TextMeshProUGUI staminaText;
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI nameLabel;
    
    [SerializeField] Button runButton;
    [SerializeField] Button switchButton;
    [SerializeField] Button abilityOneButton;
    [SerializeField] Button reloadButton;
    [SerializeField] Button grenadeButton;

    Unit currentUnit;
    bool isPlayerUnit;
    bool reloading = false;
    float reloadTime;

    void Start() {
        PlayerControl.selectedNewUnit += UpdateProfile;
        reloadButton.onClick.AddListener(ReloadButtonClicked);
        runButton.onClick.AddListener(RunButtonClicked);
        grenadeButton.onClick.AddListener(GrenadeButtonClicked);
    }

    void UpdateData() {
        hpSlider.value = currentUnit.CurrentHp;
        hpText.text = currentUnit.CurrentHp + "/" + currentUnit.stats.MaxHp;
        if (reloading) {
            ammoText.text = "Reloading (" + reloadTime.ToString("F1") + "s)";
            ammoSlider.value = ammoSlider.maxValue - reloadTime;
        }
        else {
            ammoText.text = currentUnit.CurrentAmmo + "/" + currentUnit.weapon.MaxAmmo;
            ammoSlider.value = currentUnit.CurrentAmmo;
        }
        staminaSlider.value = currentUnit.CurrentStamina;
        staminaText.text = currentUnit.CurrentStamina + "/" + currentUnit.stats.MaxStamina;
        nameLabel.text = currentUnit.UnitName;
    }
    
    void UpdateProfile(Unit unit) {
        if (unit == null) {
            panel.SetActive(false);
        }
        else {
            currentUnit = unit;
            if (unit.GetType() == typeof(SquadUnit)) {
                SquadUnit squader = (SquadUnit)unit;
                squader.updateUI += UpdateData;
                squader.startReloading += StartReloading;
                squader.reloading += Reloading;
                hpSlider.maxValue = currentUnit.stats.MaxHp;
                ammoSlider.maxValue = currentUnit.weapon.MaxAmmo;
                staminaSlider.maxValue = currentUnit.stats.MaxStamina;
                switchButton.interactable = unit.secondaryWeapon != null;
            }
            else {
                //Enemy
            }
            panel.SetActive(true);
            UpdateData();
        }
    }

    void StartReloading(float reloadTime) {
        reloading = true;
        this.reloadTime = reloadTime;   
        ammoSlider.maxValue = this.reloadTime;
        UpdateData();
        Debug.Log("start");
    }

    void Reloading(float remainingReloadTime) {
        if (reloadTime <= 0) {
            Debug.Log("stopped");
            reloading = false;
            ammoSlider.maxValue = currentUnit.weapon.MaxAmmo;
        }
        else {
            Debug.Log("reloading");
            reloadTime = remainingReloadTime;
        }
        UpdateData();
        
    }
    // these interact with the unit
    void RunButtonClicked() {
        ((SquadUnit)currentUnit).ToggleSprint();
    }

    void ReloadButtonClicked() {
        ((SquadUnit)currentUnit).ReloadNow();
    }

    void SwitchWeaponsClicked() {
        
    }
    // this one interacts with playerControl
    void GrenadeButtonClicked() {
        grenadeAction?.Invoke();
    }
}
