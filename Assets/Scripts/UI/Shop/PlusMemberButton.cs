using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlusMemberButton : UnitButton
{
    protected override int GetId() {
        return -1;
    }
    protected override Shop.types GetButtonType() {
        return Shop.types.none;
    }
}
