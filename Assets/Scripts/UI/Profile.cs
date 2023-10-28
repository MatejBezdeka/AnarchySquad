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
    Stats stats;
    Weapon weapon;

    private void Start() {
        PlayerControl.selectedNewUnit += UpdateProfile;
    }

    public void UpdateSliders() {
        hpSlider.maxValue = stats.MaxHp;
        hpSlider.value = stats.Hp;
        hpText.text = stats.Hp + "/" + stats.MaxHp;
        ammoSlider.maxValue = weapon.MaxAmmo;
        ammoSlider.value = weapon.CurrentAmmo;
        ammoText.text = weapon.CurrentAmmo + "/" + weapon.MaxAmmo;
    }

    void UpdateProfile(Unit unit) {
        if (unit == null) {
            panel.SetActive(false);
        }
        else {
            panel.SetActive(true);
            stats = unit.stats;
            weapon = unit.weapon;
            UpdateSliders();
        }
    }
}
