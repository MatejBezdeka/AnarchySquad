using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
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

    Stats stats;
    Weapon weapon;
    bool reloading = false;
    float reloadTime = 0;

    void Start() {
        PlayerControl.selectedNewUnit += UpdateProfile;
        reloadButton.onClick.AddListener(ReloadButtonClicked);
        runButton.onClick.AddListener(RunButtonClicked);
    }

    void UpdateData() {
        hpSlider.maxValue = stats.MaxHp;
        hpSlider.value = stats.Hp;
        hpText.text = stats.Hp + "/" + stats.MaxHp;
        ammoSlider.maxValue = weapon.MaxAmmo;
        if (reloading) {
            reloadTime -= Time.deltaTime;
            ammoText.text = "Reloading (" + (reloadTime).ToString("F1") + "s)";
        }
        else {
            ammoText.text = weapon.CurrentAmmo + "/" + weapon.MaxAmmo;
            ammoSlider.value = weapon.CurrentAmmo;

        }
        staminaSlider.maxValue = stats.MaxStamina;
        staminaSlider.value = stats.Stamina;
        staminaText.text = stats.Stamina + "/" + stats.MaxStamina;
        nameLabel.text = stats.UnitName;
    }
    
    void UpdateProfile(Unit unit) {
        if (unit == null) {
            panel.SetActive(false);
        }
        else {
            SquadUnit squader = (SquadUnit)unit;
            squader.updateUI += UpdateData;
            squader.startReloading += StartReloading;
            panel.SetActive(true);
            stats = unit.stats;
            weapon = unit.weapon;
            UpdateData();
        }
    }

    void StartReloading(float reloadTime) {
        if (reloadTime >= 0) {
            reloading = false;
            this.reloadTime = 0;
            UpdateData();
        }
        reloading = true;
        this.reloadTime = reloadTime;
    }
    void RunButtonClicked() {
        runAction?.Invoke();
    }

    void ReloadButtonClicked() {
        reloadAction?.Invoke();
    }
}
