using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemData : MonoBehaviour
{
    public CanvasSkinShop canvasSkinShop;

    public GameObject lockerObject;

    public int itemData;
    public bool locker;

    public void Start()
    {
        if (!locker)
        {
            lockerObject.SetActive(false);
        }
    }

    public void ItemButton()
    {
        canvasSkinShop.itemIndex = itemData;
        canvasSkinShop.OnItemClicked(locker);
    }
}
