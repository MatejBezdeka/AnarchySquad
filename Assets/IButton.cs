using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButton {
    public static Action<Settings.ButtonSounds> PlayButtonSound;
    public Settings.ButtonSounds Sound { get; }
    /* - insert
     public Settings.ButtonSounds sound;
    
    public Settings.ButtonSounds Sound {
        get { return sound; }
    }
    
    IButton.PlayButtonSound.Invoke(Sound);
     */
}

