using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsQualitySettings : ListSetting
{
    protected override void ApplySettings() {
        value = PlayerPrefs.GetInt("Gq");
        ChangeValue(value);
    }

    protected override void Load() {
        PlayerPrefs.SetInt("Gq", value);
        QualitySettings.vSyncCount = value;
    }

}
