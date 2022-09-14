using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemData : MonoBehaviour
{
    public int itemData;

    public CanvasSkinShop canvasSkinShop;

    public void ItemButton()
    {
        canvasSkinShop.itemIndex = itemData;
        canvasSkinShop.OnItemClicked();
    }
}
