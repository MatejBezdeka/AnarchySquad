using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class LoadSaveButton : MonoBehaviour, IButton {
    public AudioSettings.ButtonSounds sound;
    public AudioSettings.ButtonSounds Sound {
        get { return sound; }
    }

    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject loadSaveButton;
    [SerializeField] private TextMeshProUGUI text;
    void Start() {
        transform.GetComponent<Button>().onClick.AddListener(Load);
        Save s = Save.GetSave(0);
        if (s.missionsDone == -1) {
            loadSaveButton.SetActive(false);
            newGameButton.SetActive(true);
        }
        else {
            text.text = s.date;
        }
    }

    void Load() {
        IButton.PlayButtonSound.Invoke(Sound);
        SceneManager.LoadScene("Scenes/MissionSelector");
    }

}
