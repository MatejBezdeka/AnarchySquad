using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    [SerializeField] Button exitButton;
    [SerializeField] Button applyButton;
    public static event Action exitedSettings; 
    public static event Action apliedSettings; 
    void Start() {
        if (exitedSettings != null) exitButton.onClick.AddListener(exitedSettings.Invoke);
        if (apliedSettings != null) applyButton.onClick.AddListener(apliedSettings.Invoke);
    }
}
