using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour, IButton
{
    public static event Action grenadeAction;
    public Settings.ButtonSounds Sound { get { return Settings.ButtonSounds.normal; } }
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
    bool switching = false;
    float timeRemaining;

    void Start() {
        PlayerControl.selectedNewUnit += UpdateProfile;
        reloadButton.onClick.AddListener(ReloadButtonClicked);
        runButton.onClick.AddListener(RunButtonClicked);
        grenadeButton.onClick.AddListener(GrenadeButtonClicked);
        switchButton.onClick.AddListener(SwitchWeaponsClicked);
    }

    void Update() {
        
    }

    void UpdateData() {
        hpSlider.value = currentUnit.CurrentHp;
        hpText.text = currentUnit.CurrentHp + "/" + currentUnit.stats.MaxHp;
        if (reloading && (int)timeRemaining != -1) {
            ammoText.text = "Reloading (" + timeRemaining.ToString("F1") + "s)";
            ammoSlider.value = ammoSlider.maxValue - timeRemaining;
        }else if (switching && (int)timeRemaining != -1) {
            ammoText.text = "Switching (" + timeRemaining.ToString("F1") + "s)";
            ammoSlider.value = ammoSlider.maxValue - timeRemaining;
        }
        else {
            ammoSlider.maxValue = currentUnit.weapon.MaxAmmo;
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
                squader.startSwitching += StartSwitching;
                squader.switching += Switching;
                hpSlider.maxValue = currentUnit.stats.MaxHp;
                ammoSlider.maxValue = currentUnit.weapon.MaxAmmo;
                staminaSlider.maxValue = currentUnit.stats.MaxStamina;
                switchButton.interactable = squader.secondaryWeapon;
            }
            else {
                //Enemy
            }
            panel.SetActive(true);
            UpdateData();
        }
    }

    void DeselectUnit(SquadUnit unit) {
        unit.updateUI -= UpdateData;
        unit.startReloading -= StartReloading;
        unit.reloading -= Reloading;
        unit.switching -= Switching;
        unit.startSwitching -= StartSwitching;
    }
    void StartReloading(float reloadTime) {
        reloading = true;
        switching = false;
        this.timeRemaining = reloadTime;   
        ammoSlider.maxValue = this.timeRemaining;
        ammoSlider.value = currentUnit.CurrentAmmo;
        UpdateData();
    }
    
    void Reloading(float remainingReloadTime) {
        if (timeRemaining <= 0) {
            reloading = false;
            ammoSlider.maxValue = currentUnit.weapon.MaxAmmo;
            ammoSlider.value = currentUnit.CurrentAmmo;
        }
        else {
            timeRemaining = remainingReloadTime;
        }
        UpdateData();
        
    }

    void StartSwitching(float switchTime) {
        switching = true;
        reloading = false;
        this.timeRemaining = switchTime;   
        ammoSlider.maxValue = this.timeRemaining;
        ammoSlider.value = currentUnit.CurrentAmmo;
        UpdateData();
    }

    void Switching(float remainingSwitchTime) {
        if (timeRemaining <= 0) {
            switching = false;
            ammoSlider.maxValue = currentUnit.weapon.MaxAmmo;
            ammoSlider.value = currentUnit.CurrentAmmo;
        }
        else {
            timeRemaining = remainingSwitchTime;
        }
        UpdateData();
    }
    // these interact with the unit
    void RunButtonClicked() {
        ((SquadUnit)currentUnit).ToggleSprint();
        IButton.PlayButtonSound.Invoke(Sound);
    }

    void ReloadButtonClicked() {
        ((SquadUnit)currentUnit).ReloadNow();
        IButton.PlayButtonSound.Invoke(Sound);
    }

    void SwitchWeaponsClicked() {
        ((SquadUnit)currentUnit).StartSwitchingWeaponsNow();
        IButton.PlayButtonSound.Invoke(Sound);
    }
    // this one interacts with playerControl
    void GrenadeButtonClicked() {
        grenadeAction?.Invoke();
        IButton.PlayButtonSound.Invoke(Sound);
    }
}
