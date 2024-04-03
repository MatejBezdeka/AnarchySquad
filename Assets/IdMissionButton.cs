using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class IdMissionButton : MonoBehaviour, IButton {
    public int id;
    public static event Action<int> ButtonClicked; 
    public static event Action<int> ButtonPointerEntered; 
    public static event Action ButtonPointerLeft; 
    public Settings.ButtonSounds Sound => Settings.ButtonSounds.normal;
    void Start() {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Clicked);

    }
    public void PointerEnter() {
        ButtonPointerEntered!.Invoke(id);
    }
    public void PointerExit() {
        ButtonPointerLeft!.Invoke();
    }
    void Clicked() {
        IButton.PlayButtonSound.Invoke(Sound);
        ButtonClicked!.Invoke(id); 
    }

}
