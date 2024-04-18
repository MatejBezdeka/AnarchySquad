using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsQualitySettings : ListSetting
{
    protected override void ApplySettings() {
        PlayerPrefs.SetInt("Gq", value);
        QualitySettings.SetQualityLevel(value);
    }

    protected override void Load() {
        value = PlayerPrefs.GetInt("Gq");
        ChangeValue(value);
    }

}
