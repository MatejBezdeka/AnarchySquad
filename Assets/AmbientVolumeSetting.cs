using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbientVolumeSetting : SliderSetting
{
    protected override void ChangedValue(float val) {
        base.ChangedValue(val);
        label.text = slider.value + "%";
    }

    protected override void ApplySettings() {
        PlayerPrefs.SetInt("Av", (int)slider.value);
        AudioSettings.Music.ChangeVolume(AudioSettings.AudioGroups.Ambient, slider.value);
        
    }

    protected override void Load() {
        value = PlayerPrefs.GetInt("Av");
        slider.value = value;
        ChangedValue(value);
    }
}
