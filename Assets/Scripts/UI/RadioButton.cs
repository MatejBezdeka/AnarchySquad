using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RadioButton : MonoBehaviour {
    Button button;
    [SerializeField] List<RadioButton> buttonsInGroup;
    [SerializeField] bool enabledFromStart = true;
    [SerializeField] GameObject childObject;
    public bool Enabled {
        get => button.interactable;
        set {
            button.interactable = value;
            childObject.SetActive(!value);
        } }
    void Start() {
        if (buttonsInGroup.Count == 0) {
            Debug.LogWarning("Empty radio button");
        }

        if (childObject == null) {
            Debug.LogWarning("no child object");
        }
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
        button.interactable = enabledFromStart;
        Enabled = enabledFromStart;
        childObject.SetActive(!enabledFromStart);
    }

    void Clicked() {
        foreach (RadioButton btn in buttonsInGroup) {
            btn.Enabled = true;
        }
        Enabled = false;
    }
}
