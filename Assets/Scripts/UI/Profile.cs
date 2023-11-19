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
    float reloadTime = 0;

    void Start() {
        PlayerControl.selectedNewUnit += UpdateProfile;
        reloadButton.onClick.AddListener(ReloadButtonClicked);
        runButton.onClick.AddListener(RunButtonClicked);
        grenadeButton.onClick.AddListener(GrenadeButtonClicked);

    }

    void UpdateData() {
        hpSlider.maxValue = currentUnit.stats.MaxHp;
        hpSlider.value = currentUnit.stats.Hp;
        hpText.text = currentUnit.stats.Hp + "/" + currentUnit.stats.MaxHp;
        ammoSlider.maxValue = currentUnit.weapon.MaxAmmo;
        if (reloading) {
            reloadTime -= Time.deltaTime;
            ammoText.text = "Reloading (" + (reloadTime).ToString("F1") + "s)";
        }
        else {
            ammoText.text = currentUnit.weapon.CurrentAmmo + "/" + currentUnit.weapon.MaxAmmo;
            ammoSlider.value = currentUnit.weapon.CurrentAmmo;

        }
        staminaSlider.maxValue = currentUnit.stats.MaxStamina;
        staminaSlider.value = currentUnit.stats.Stamina;
        staminaText.text = currentUnit.stats.Stamina + "/" + currentUnit.stats.MaxStamina;
        nameLabel.text = currentUnit.stats.UnitName;
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
            }
            else {
                
            }
            panel.SetActive(true);
            UpdateData();
        }
    }

    void StartReloading(float reloadTime) {
        if (reloadTime <= 0) {
            reloading = false;
        }
        else {
            reloading = true;
            this.reloadTime = reloadTime;   
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
    // this one interacts with playerControl
    void GrenadeButtonClicked() {
        grenadeAction?.Invoke();
    }
}
