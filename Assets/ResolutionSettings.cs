using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSettings : ListSetting
{
    protected override void Start() {
        base.Start();
        int hz = Screen.resolutions[0].refreshRate;
        foreach (var resolution in Screen.resolutions) {
            if (resolution.refreshRate != hz) continue;
            items.Add(resolution.width + "x" + resolution.height);
        }
        label.text = items[value];
    }

    protected override void ApplySettings() {
        base.ApplySettings();
        var dimensions = items[value].Split("x");
        Screen.SetResolution(int.Parse(dimensions[0]), int.Parse(dimensions[0]), false);
        PlayerPrefs.SetInt("W", int.Parse(dimensions[0]));
        PlayerPrefs.SetInt("H", int.Parse(dimensions[1]));
    }

    protected override void RevertSettings() {
        base.RevertSettings();
        label.text = items[previousValue];
    }
}
