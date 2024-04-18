using System;
using UnityEngine;

public abstract class UnitButton : UIButton {
    public int Id => GetId();
    public static event Action<Tuple<int, Shop.types >> clickedUnitButton;
    protected abstract int GetId();
    protected abstract Shop.types GetButtonType();
    protected override void Start() {
        base.Start();
        Debug.Log("haya");
    }

    protected override void Functionality() {
        IButton.PlayButtonSound.Invoke(Sound);
        clickedUnitButton!.Invoke(new Tuple<int, Shop.types>(GetId(), GetButtonType())); 
    }
}
