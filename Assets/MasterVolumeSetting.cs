using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterVolumeSetting : SliderSetting
{
    protected override void ChangedValue(float val) {
        base.ChangedValue(val);
        label.text = slider.value + "%";
    }

    protected override void ApplySettings() {
        PlayerPrefs.SetInt("MaV", (int)slider.value);
        AudioSettings.Music.ChangeVolume(AudioSettings.AudioGroups.Master, slider.value);
        
    }

    protected override void Load() {
        value = PlayerPrefs.GetInt("MaV");
        slider.value = value;
        ChangedValue(value);
    }
}
