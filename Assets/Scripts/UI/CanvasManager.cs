using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using World;

public class CanvasManager : MonoBehaviour {
    public static event Action grenadeAction;
    public static event Action reloadAction; 
    public static event Action runAction; 
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
    float timeCooldown = 0;
    int seconds = 0;
    int minutes = 0;
    //[SerializeField] Button abilityTwoButton;
    

    void Start() {
        portraits = new List<Portrait>();
        foreach (var unit in GameManager.instance.Squaders) {
            GameObject portrait = Instantiate(portraitPrefab, portraitContainer.transform);
            portraits.Add(portrait.GetComponent<Portrait>());
            portraits[^1].AssignUnit(unit);
        }
        grenadeButton.onClick.AddListener(GrenadeButtonClicked);
        reloadButton.onClick.AddListener(ReloadButtonClicked);
        runButton.onClick.AddListener(RunButtonClicked);
    }

    void Update() {
        timeCooldown += Time.deltaTime;
        if (timeCooldown >= 1) {
            timeCooldown -= 1;
            seconds++;
            if (seconds == 60) {
                minutes++;
                seconds = 0;
                ChangeTimeLabelText( minutes + ":" + seconds);
            }
        }
    }
    
    public void ChangeTimeLabelText(string text) {
        timeText.text = text;
    }

    void GrenadeButtonClicked() {
        grenadeAction?.Invoke();
    }

    void RunButtonClicked() {
        runAction?.Invoke();
        Debug.Log("not implemeted yet");
    }

    void ReloadButtonClicked() {
        reloadAction?.Invoke();
        Debug.Log("not implemeted yet");
    }

    void SwitchButtonClicked() {
        Debug.Log("not implemeted yet");
    }

    void AbilityButtonClicked() {
        Debug.Log("not implemeted yet");
    }
}
