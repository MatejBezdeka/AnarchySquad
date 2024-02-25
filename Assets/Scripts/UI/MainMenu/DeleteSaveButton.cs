using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class DeleteSaveButton : MonoBehaviour, IButton
{
    public Settings.ButtonSounds sound;
    public Settings.ButtonSounds Sound { get { return sound; } }
    void Start()
    {
        
    }

    void Clicked()
    {
        IButton.PlayButtonSound.Invoke(Sound);
    }
}
