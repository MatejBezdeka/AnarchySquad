using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RadioButton : UIButton {
    [SerializeField] List<RadioButton> buttonsInGroup;
    [SerializeField] bool enabledFromStart = true;
    [SerializeField] GameObject childObject;
    public bool Enabled {
        get => button.interactable;
        set {
            button.interactable = value;
            childObject.SetActive(!value);
        } }

    protected override void Start() {
        base.Start();
        if (buttonsInGroup.Count == 0) {
            Debug.LogWarning("Empty radio button");
        }

        if (childObject == null) {
            Debug.LogWarning("no child object");
        }
        button.interactable = enabledFromStart;
        Enabled = enabledFromStart;
        childObject.SetActive(!enabledFromStart);
    }
    protected override void Functionality() {
        foreach (RadioButton btn in buttonsInGroup) {
            btn.Enabled = true;
        }
        IButton.PlayButtonSound.Invoke(Sound);
        Enabled = false;
    }
}
