using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackHubButton : MonoBehaviour, IButton
{
    public Settings.ButtonSounds sound;
    public Settings.ButtonSounds Sound { get { return sound; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Clicked() {
        IButton.PlayButtonSound.Invoke(Sound);
    }
}
