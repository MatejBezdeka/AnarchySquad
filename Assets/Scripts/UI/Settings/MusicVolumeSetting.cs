using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolumeSetting : SliderSetting
{
    protected override void ChangedValue(float val) {
        base.ChangedValue(val);
        label.text = slider.value + "%";
    }

    protected override void ApplySettings() {
        Debug.Log("A");
        PlayerPrefs.SetInt("MuV", (int)slider.value);
        AudioSettings.Music.ChangeVolume(AudioSettings.AudioGroups.Music, slider.value);
        
    }

    protected override void Load() {
        Debug.Log("B");
        value = PlayerPrefs.GetInt("MuV");
        slider.value = value;
        ChangedValue(value);
    }
}
