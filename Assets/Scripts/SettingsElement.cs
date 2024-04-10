using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class SettingsElement : MonoBehaviour {
    [SerializeField] protected TextMeshProUGUI label;
    [SerializeField] protected int value;
    protected virtual void Start() {
        Settings.apliedSettings += ApplySettings;
        Settings.loadSettings += Load;
    }
    protected abstract void ApplySettings();
    void OnDestroy() {
        Settings.apliedSettings -= ApplySettings;
    }
    protected abstract void Load();
}
