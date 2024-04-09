using System;
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

    private void OnEnable() {
        int width = PlayerPrefs.GetInt("W");
        int height =  PlayerPrefs.GetInt("H");
        for (int i = 0; i < items.Count; i++) {
            string resolution = items[i];
            if (width + "x" + height == resolution) {
                ChangeValue(i);
                return;
            }
        }
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
