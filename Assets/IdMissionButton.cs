using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class IdMissionButton : UIButton {
    public int id = 0;
    public static event Action<int> ButtonClicked; 
    public static event Action<int> ButtonPointerEntered; 
    public static event Action ButtonPointerLeft; 
    
    public void PointerEnter() {
        ButtonPointerEntered!.Invoke(id);
    }
    public void PointerExit() {
        ButtonPointerLeft!.Invoke();
    }
    protected override void Functionality() {
        ButtonClicked!.Invoke(id); 
    }
}
