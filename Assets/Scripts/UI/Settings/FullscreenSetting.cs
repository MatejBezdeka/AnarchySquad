using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenSetting : ListSetting
{

    protected override void ApplySettings() {
        PlayerPrefs.SetInt("Fs", value);
        Screen.fullScreen = value == 1;
    }

    protected override void Load() {
        value = PlayerPrefs.GetInt("Fs");
        ChangeValue(value);
    }
}
