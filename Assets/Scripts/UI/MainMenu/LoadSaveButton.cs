using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class LoadSaveButton : MonoBehaviour, IButton {
    public Settings.ButtonSounds sound;
    public Settings.ButtonSounds Sound {
        get { return sound; }
    }

    void Start() {
        transform.GetComponent<Button>().onClick.AddListener(Load);
    }

    void Load() {
        IButton.PlayButtonSound.Invoke(Sound);
        SceneManager.LoadScene("Hub");
    }

}
