using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlusMemberButton : UnitButton
{
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    protected override int GetId() {
        return -1;
    }

    protected override Shop.types GetButtonType() {
        return Shop.types.none;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        button.onClick.RemoveAllListeners();
    }
}
