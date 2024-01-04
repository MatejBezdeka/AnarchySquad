using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class IUnitButton : MonoBehaviour {
    public static event Action<SquadUnit> clickedUnitButton;
    protected virtual void Start() {
        GetComponent<Button>().onClick.AddListener(Clicked);
    }

    void Clicked() {
        clickedUnitButton!.Invoke(ReturnUnit());    
    }

    protected abstract SquadUnit ReturnUnit();
}
