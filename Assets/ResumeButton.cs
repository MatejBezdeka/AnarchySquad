using System.Collections;
using System.Collections.Generic;
using Camera;
using UnityEngine;

public class ResumeButton : UIButton {
    [SerializeField] GameObject gameObjectToHide;
    protected override void Start() {
        base.Start();
        PauseState.resumeGame += Functionality;
    }

    protected override void Functionality() {
        gameObjectToHide.SetActive(false);
        GameManager.instance.ChangeTime(-3);
    }
}

