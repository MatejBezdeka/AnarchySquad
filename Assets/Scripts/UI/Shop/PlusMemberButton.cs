using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlusMemberButton : IUnitButton
{
    protected override SquadUnit ReturnUnit() {
        return null;
    }
}
