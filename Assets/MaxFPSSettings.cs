using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxFPSSettings : SliderSetting {
    
    protected override void Start()
    {
        base.Start();
    }

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
        previousValue = slider.value;
    }

    protected override void RevertSettings() {
        slider.value = previousValue;
        ChangedValue(slider.value);
    }
}
