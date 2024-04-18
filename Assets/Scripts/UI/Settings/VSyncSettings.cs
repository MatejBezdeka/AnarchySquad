using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSyncSettings : ListSetting
{
    protected override void Load() {
        value = PlayerPrefs.GetInt("Vs");
        ChangeValue(value);
    }
    protected override void ApplySettings() {
        PlayerPrefs.SetInt("Vs", value);
        QualitySettings.vSyncCount = value;
    }
    
}
