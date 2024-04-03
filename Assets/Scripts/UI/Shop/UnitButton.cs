using System;
using UnityEngine;
using UnityEngine.UI;
public abstract class UnitButton : MonoBehaviour, IButton {
    public int Id => GetId();
    protected Button button;
    public Settings.ButtonSounds Sound => Settings.ButtonSounds.normal;
    public static event Action<Tuple<int, Shop.types >> clickedUnitButton;
    protected abstract int GetId();
    protected abstract Shop.types GetButtonType();
    protected void Clicked() {
        IButton.PlayButtonSound.Invoke(Sound);
        clickedUnitButton!.Invoke(new Tuple<int, Shop.types>(GetId(), GetButtonType())); 
    }
}
