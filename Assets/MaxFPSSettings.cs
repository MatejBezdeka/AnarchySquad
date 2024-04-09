using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxFPSSettings : SliderSetting {
    protected override void ChangedValue(float val) {
        base.ChangedValue(val);
        if (slider.value == slider.maxValue) {
            label.text = "Unlimited";
        }
    }

    protected override void ApplySettings() {
        if (slider.value == slider.maxValue) {
            Application.targetFrameRate = -1;
        }
        else {
            Application.targetFrameRate = value;
        }
        PlayerPrefs.SetInt("Fr", (int)slider.value);
    }
    protected override void Load() {
        value = PlayerPrefs.GetInt("Fr");
        slider.value = value;
        ChangedValue(value);
    }
}
