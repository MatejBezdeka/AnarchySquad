using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeVisibilityButton : UIButton {
    [SerializeField] GameObject gameObjectToHide;
    [SerializeField] GameObject gameObjectToShow;
    protected override void Functionality() {
        gameObjectToShow.SetActive(true);
        gameObjectToHide.SetActive(false);
    }
}
