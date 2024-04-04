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
    [SerializeField] TextMeshProUGUI timerText;
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
    bool continueClock = true;
    //[SerializeField] Button abilityTwoButton;
    

    void Start() {
        portraits = new List<Portrait>();
        StartCoroutine(Clock());
        if (GameManager.instance.Units == null) {
            return;
        }
        foreach (var unit in GameManager.instance.Units) {
            GameObject portrait = Instantiate(portraitPrefab, portraitContainer.transform);
            portraits.Add(portrait.GetComponent<Portrait>());
            portraits[^1].AssignUnit(unit);
        }
        
    }

    IEnumerator Clock() {
        WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(1);
        //WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (continueClock) {
            seconds++;
            if (seconds == 60) {
                minutes++;
                seconds = 0; 
            }
            ChangeTimerLabelText(minutes + ":" + (seconds < 10? "0" : "")+ seconds);
            yield return waitForSecondsRealtime;
        }
    }
    
    public void ChangeTimeLabelText(string text) {
        timeText.text = text;
    }

    void ChangeTimerLabelText(string text) {
        timerText.text = text;
    }

    public void StartTimer() {
        continueClock = true;
        StartCoroutine(Clock());
    }
    public void StopTimer() {
        continueClock = false;
    }

    

    

    void SwitchButtonClicked() {
        Debug.Log("not implemeted yet");
    }

    void AbilityButtonClicked() {
        Debug.Log("not implemeted yet");
    }
}
