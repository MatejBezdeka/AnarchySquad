using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ListSetting : SettingsElement {
    [SerializeField] Button nextButton;
    [SerializeField] Button previousButton;
    [SerializeField] bool cycleable;
    [SerializeField] protected List<string> items;
    protected override void Start() {
        base.Start();
        nextButton.onClick.AddListener(NextOption);   
        previousButton.onClick.AddListener(PreviousOption);
        if (!cycleable) {
            previousButton.interactable = value != 0;
        }
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
        do {
            if (!cycleable) {
                previousButton.interactable = value != 0;
                nextButton.interactable = value != items.Count-1;
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
        } while (newValue != value);
        label.text = items[value];
    }
}
