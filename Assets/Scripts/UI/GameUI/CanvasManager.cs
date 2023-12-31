using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using World;

public class CanvasManager : MonoBehaviour {
    
    [Header("Objective & Time")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI objectiveText;
    [Header("Portraits")]
    [SerializeField] GameObject portraitContainer;
    [SerializeField] GameObject portraitPrefab;
    List<Portrait> portraits;
    Profile profile;
    [Header("Buttons")] 
    
    float timeCooldown = 0;
    int seconds = 0;
    int minutes = 0;
    //[SerializeField] Button abilityTwoButton;
    

    void Start() {
        portraits = new List<Portrait>();
        if (GameManager.instance.units == null) {
            return;
        }
        foreach (var unit in GameManager.instance.units) {
            GameObject portrait = Instantiate(portraitPrefab, portraitContainer.transform);
            portraits.Add(portrait.GetComponent<Portrait>());
            portraits[^1].AssignUnit(unit);
        }
        
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

    

    

    void SwitchButtonClicked() {
        Debug.Log("not implemeted yet");
    }

    void AbilityButtonClicked() {
        Debug.Log("not implemeted yet");
    }
}
