using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTeamMemberContainer : MonoBehaviour {
    public static event Action<SquadUnit> RemoveUnit;
    private SquadUnit unit;
    [SerializeField] Button removeButton;
    [SerializeField] TextMeshProUGUI unitName;
    [SerializeField] TextMeshProUGUI className;
    [SerializeField] Image unitImage;
    void Start()
    {
        removeButton.onClick.AddListener(RemoveButtonClicked);
    }
    public void Set(SquadUnit unit) {
        this.unit = unit;
        unitName.text = name;
        //get name
        //get class
        //get img
    }
    void RemoveButtonClicked() {
        RemoveUnit?.Invoke(unit);
        Destroy(gameObject);
    }
}