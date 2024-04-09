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
    protected override void ApplySettings() {
        var dimensions = items[value].Split("x");
        Screen.SetResolution(int.Parse(dimensions[0]), int.Parse(dimensions[0]), false);
        PlayerPrefs.SetInt("Rw", int.Parse(dimensions[0]));
        PlayerPrefs.SetInt("Rh", int.Parse(dimensions[1]));
    }

    protected override void Load() {
        int width = PlayerPrefs.GetInt("Rw");
        int height =  PlayerPrefs.GetInt("Rh");
        for (int i = 0; i < items.Count; i++) {
            string resolution = items[i];
            if (width + "x" + height == resolution) {
                ChangeValue(i);
                return;
            }
        }
    }
}
