using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using World;

public class CanvasManager : MonoBehaviour {
    public static event Action grenadeAction; 
    [Header("Objective & Time")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI objectiveText;
    [Header("Portraits")]
    [SerializeField] GameObject portraitContainer;
    [SerializeField] GameObject portraitPrefab;
    List<Portrait> portraits;
    Profile profile;
    [Header("Buttons")] 
    [SerializeField] Button reloadButton;
    [SerializeField] Button grenadeButton;
    [SerializeField] Button runButton;
    [SerializeField] Button switchButton;
    [SerializeField] Button abilityOneButton;
    //[SerializeField] Button abilityTwoButton;
    

    void Start() {
        portraits = new List<Portrait>();
        foreach (var unit in GameManager.instance.Squaders) {
            GameObject portrait = Instantiate(portraitPrefab, portraitContainer.transform);
            portraits.Add(portrait.GetComponent<Portrait>());
            portraits[^1].AssignUnit(unit);
        }
        grenadeButton.onClick.AddListener(GrenadeButtonClicked);
    }

    void Update()
    {
    }

    public void ChangeTimeLabelText(string text) {
        timeText.text = text;
    }

    void GrenadeButtonClicked() {
        grenadeAction?.Invoke();
    }
}
