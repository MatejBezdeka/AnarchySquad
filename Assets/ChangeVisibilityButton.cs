using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeVisibilityButton : MonoBehaviour, IButton {
    [SerializeField] GameObject gameObjectToHide;
    [SerializeField] GameObject gameObjectToShow;
    public Settings.ButtonSounds sound;
    public Settings.ButtonSounds Sound { get; }
    void Start() {
        transform.GetComponent<Button>().onClick.AddListener(Click);
    }

    void Click() {
        IButton.PlayButtonSound.Invoke(Sound);
        gameObjectToShow.SetActive(true);
        gameObjectToHide.SetActive(false);
    }
}
