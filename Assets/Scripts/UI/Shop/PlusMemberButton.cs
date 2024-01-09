using System.Collections;
using System.Collections.Generic;
using UI.Shop;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlusMemberButton : IUnitButton
{
    
    protected override SquadUnit ReturnUnit() {
        return null;
    }

    protected override int GetId() {
        return -1;
    }
}
