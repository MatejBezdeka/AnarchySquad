using System;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTeamMemberContainer : UnitButton {
    public static event Action<int> RemoveUnit;
    public static event Action<Tuple<Shop.types, int>> RemoveElement;
    public static event Action hideDescription;
    public static event Action<int> showUnitDescription;
    int id;
    Shop.types clickedButton;
    [Header("PlaceHolders")]
    [SerializeField] Sprite placeholderStatsImg;
    [SerializeField] Sprite placeholderWeaponImg;
    [SerializeField] string placeHolderName;
    [SerializeField] Button removeButton;
    [Header("Stats")]
    [SerializeField] TextMeshProUGUI unitName;
    [SerializeField] Button statsAddButton;
    [SerializeField] Button statsRemoveButton;
    [SerializeField] GameObject statsRButton;
    //[SerializeField] TextMeshProUGUI className;
    [SerializeField] Image statsImage;
    [Header("Weapons")]
    [SerializeField] Image weaponImg;
    [SerializeField] Button weaponAddButton;
    [SerializeField] Button weaponRemoveButton;
    [SerializeField] GameObject weaponRButton;
    [SerializeField] Image secondaryWeaponImg;
    [SerializeField] Button secondaryWeaponAddButton;
    [SerializeField] Button secondaryWeaponRemoveButton;
    [SerializeField] GameObject secondaryWeaponRButton;
    //[SerializeField] string placeHolderClass;
    
    protected void Start() {
        removeButton.onClick.AddListener(RemoveButtonClicked);
        statsAddButton.onClick.AddListener(() => { clickedButton = Shop.types.unit; Clicked(); });
        statsRemoveButton.onClick.AddListener(RemoveStats);
        weaponAddButton.onClick.AddListener(() => { clickedButton = Shop.types.weapon; Clicked(); });
        weaponRemoveButton.onClick.AddListener(RemoveWeapon);
        secondaryWeaponAddButton.onClick.AddListener(() => { clickedButton = Shop.types.secondaryWeapon; Clicked(); });
        secondaryWeaponRemoveButton.onClick.AddListener(RemoveSecondaryWeapon);
        statsImage.sprite = placeholderStatsImg;
        unitName.text = placeHolderName;
        
    }

    protected override int GetId() {
        return id;
    }

    protected override Shop.types GetButtonType() {
        return clickedButton;
    }

    public void SetId(int newId) {
        id = newId;
    }
    
    public void SetStats(string name, Sprite icon) {
        unitName.text = name;
        statsImage.sprite = icon;
        statsRButton.SetActive(true);
    }

    void RemoveStats() {
        unitName.text = placeHolderName;
        statsImage.sprite = placeholderStatsImg;
        statsRButton.SetActive(false);
        RemoveElement!.Invoke(new Tuple<Shop.types, int>(Shop.types.unit, id));
        IButton.PlayButtonSound.Invoke(AudioSettings.ButtonSounds.error);
    }
    public void SetWeapon(Weapon weapon) {
        weaponImg.sprite = weapon.Icon;
        weaponRButton.SetActive(true);
    }

    void RemoveWeapon() {
        weaponImg.sprite = placeholderWeaponImg;
        weaponRButton.SetActive(false);
        RemoveElement!.Invoke(new Tuple<Shop.types, int>(Shop.types.weapon, id));
        IButton.PlayButtonSound.Invoke(AudioSettings.ButtonSounds.error);
    }
    public void SetSecondaryWeapon(Weapon weapon) {
        secondaryWeaponImg.sprite = weapon.Icon;
       secondaryWeaponRButton.SetActive(true);
    }
    void RemoveSecondaryWeapon() {
        secondaryWeaponImg.sprite = placeholderWeaponImg;
        secondaryWeaponRButton.SetActive(false);
        RemoveElement!.Invoke(new Tuple<Shop.types, int>(Shop.types.secondaryWeapon, id));
        IButton.PlayButtonSound.Invoke(AudioSettings.ButtonSounds.error);
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
    void RemoveButtonClicked() {
        RemoveUnit?.Invoke(id);
        IButton.PlayButtonSound.Invoke(AudioSettings.ButtonSounds.error);
        Destroy(gameObject);
    }
    public void PointerEnter() {
        showUnitDescription!.Invoke(id);
    }
    public void PointerExit() {
        hideDescription!.Invoke();
    }

    private void OnDestroy() {
        removeButton.onClick.RemoveAllListeners();
        statsAddButton.onClick.RemoveAllListeners();
        statsRemoveButton.onClick.RemoveAllListeners();
        weaponAddButton.onClick.RemoveAllListeners();
        weaponRemoveButton.onClick.RemoveAllListeners();
        secondaryWeaponAddButton.onClick.RemoveAllListeners();
        secondaryWeaponRemoveButton.onClick.RemoveAllListeners();
        if (button != null) {
            button.onClick.RemoveAllListeners();
        }
    }
}
