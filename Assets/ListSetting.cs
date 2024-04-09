using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ListSetting : SettingsElement {
    [SerializeField] Button nextButton;
    [SerializeField] Button previousButton;
    [SerializeField] bool cycleable;
    [SerializeField] protected List<string> items;
    protected int previousValue;
    protected override void Start() {
        base.Start();
        nextButton.onClick.AddListener(NextOption);   
        previousButton.onClick.AddListener(PreviousOption);
        previousButton.interactable = value != 0 && !cycleable;
    }

    void NextOption() {
        value++;
        if (cycleable) {
            if (value == items.Count) {
                value = 0;
            }   
        }
        else {
            nextButton.interactable = value != items.Count-1;
            previousButton.interactable = value != 0;
        }
        label.text = items[value];
    }

    void PreviousOption() {
        value--;
        if (cycleable) {
            if (value == -1) {
                value = items.Count - 1;
            }   
        }
        else {
            previousButton.interactable = value != 0;
            nextButton.interactable = value != items.Count-1;
        }
        label.text = items[value];
    }

    protected void ChangeValue(int newValue) {
        if (newValue == value) {
            previousButton.interactable = value != 0;
            nextButton.interactable = value != items.Count-1;
            label.text = items[value];
            return;
        } 
        if (newValue < value) {
            for (int i = 0; i < value - newValue; i++) {
                PreviousOption();
            }
        }else if (newValue > value) {
            for (int i = 0; i < newValue - value; i++) {
                NextOption();
            }
        }
    }
    protected override void RevertSettings() {
        value = previousValue;
        //label.text = items[value];
    }

    protected override void ApplySettings() {
        previousValue = value;
    }
}
