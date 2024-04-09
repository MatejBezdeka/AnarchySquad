using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    [SerializeField] Button applyButton;
    public static event Action apliedSettings;
    public static event Action loadSettings;
    void Start() {
        applyButton.onClick.AddListener(ApplyButtonClicked);
    }

    void ApplyButtonClicked() {
        apliedSettings?.Invoke();
    }
    void OnEnable() {
        loadSettings?.Invoke();
    }
}
