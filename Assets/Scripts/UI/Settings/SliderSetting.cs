using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public abstract class SliderSetting : SettingsElement {
    [SerializeField] protected Slider slider;
    [SerializeField] float maxValue;
    [SerializeField] float minValue;
    [SerializeField] bool wholeNumbers;
    
    protected override void Start() {
        base.Start();
        slider.onValueChanged.AddListener(ChangedValue);
        slider.wholeNumbers = wholeNumbers;
        slider.maxValue = maxValue;
        slider.minValue = minValue;
        slider.value = value;
    }

    protected virtual void ChangedValue(float val) {
        if (wholeNumbers) {
            label.text = slider.value.ToString(CultureInfo.InvariantCulture);
        }
        else {
            label.text = slider.value.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}
