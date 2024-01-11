using System;
using UnityEngine;
using UnityEngine.UI;
public abstract class IUnitButton : MonoBehaviour {
    public int Id => GetId();
    public static event Action<Tuple<int, Shop.types >> clickedUnitButton;
    protected abstract int GetId();
    protected abstract Shop.types GetButtonType();
    protected void Clicked() {
        clickedUnitButton!.Invoke(new Tuple<int, Shop.types>(GetId(), GetButtonType())); 
    }
}
