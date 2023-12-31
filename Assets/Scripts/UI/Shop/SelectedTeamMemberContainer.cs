using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTeamMemberContainer : IUnitButton {
    public static event Action<SquadUnit> RemoveUnit;
    private SquadUnit unit;
    [SerializeField] Button removeButton;
    [SerializeField] TextMeshProUGUI unitName;
    [SerializeField] TextMeshProUGUI className;
    [SerializeField] Image unitImage;
    protected override void Start()
    {
        base.Start();
        removeButton.onClick.AddListener(RemoveButtonClicked);
    }

    protected override SquadUnit ReturnUnit() {
        return unit;
    }

    public void Set(SquadUnit unit) {
        this.unit = unit;
        unitName.text = unit.stats.UnitName;
        unitImage.sprite = unit.stats.Icon;
        className.text = unit.stats.UnitClass.ToString();
    }
    void RemoveButtonClicked() {
        RemoveUnit?.Invoke(unit);
        Destroy(gameObject);
    }
}
