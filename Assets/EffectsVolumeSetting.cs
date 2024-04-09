using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsVolumeSetting : SliderSetting
{
    protected override void ChangedValue(float val) {
        base.ChangedValue(val);
        label.text = slider.value + "%";
    }

    protected override void ApplySettings() {
        PlayerPrefs.SetInt("Ev", (int)slider.value);
        AudioSettings.Music.ChangeVolume(AudioSettings.AudioGroups.Effects, slider.value);
        
    }

    protected override void Load() {
        value = PlayerPrefs.GetInt("Ev");
        slider.value = value;
        ChangedValue(value);
    }
}
