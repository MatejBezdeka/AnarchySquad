using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Item")]
public class ShopItem : ShopItemBase
{
    //Item
    protected override string GetItemName() {
        return "not implemented";
    }

    public override Sprite GetSprite() {
        return null;
    }
}
