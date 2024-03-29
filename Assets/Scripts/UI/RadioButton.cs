using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RadioButton : MonoBehaviour, IButton {
    Button button;
    [SerializeField] List<RadioButton> buttonsInGroup;
    [SerializeField] bool enabledFromStart = true;
    [SerializeField] GameObject childObject;
    // sound
    public Settings.ButtonSounds sound;
    public Settings.ButtonSounds Sound { get { return sound; } }
    
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
        IButton.PlayButtonSound.Invoke(Sound);
        Enabled = false;
    }
}
