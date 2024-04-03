using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class UIButton : MonoBehaviour, IButton {
    public Settings.ButtonSounds sound;
    public Settings.ButtonSounds Sound => sound;
    protected Button button;
    protected virtual void Start() {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    void Clicked() {
        IButton.PlayButtonSound.Invoke(Sound);
        Functionality();
    }

    protected abstract void Functionality();
    private void OnDestroy() {
        button.onClick.RemoveAllListeners();
    }
}