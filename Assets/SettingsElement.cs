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
        Settings.exitedSettings += RevertSettings;
    }
    protected abstract void ApplySettings();
    protected abstract void RevertSettings();

    void OnDestroy() {
        Settings.apliedSettings -= ApplySettings;
        Settings.exitedSettings -= RevertSettings;
    }
}
