using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopItem : MonoBehaviour
{
    DataManager dataIns;
    ItemListData itemData;

    public CanvasSkinShop canvasSkinShop;
    public GetItemData getItemData;

    public Image itemImage;
    public GameObject hatItemHolder, pantItemHolder , item, uiHolder, locker;

    public int itemIndex, loopCounter;
    
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        dataIns = DataManager.Ins;
        itemData = dataIns.itemListData;

        for (int i = 0; i < itemData.hatDatasList.Count; i++)
        {
            SetItem(hatItemHolder.transform, itemData.hatDatasList[i].hatSprite, itemData.hatDatasList[i].locked, i);
        }
        
        for (int i = 0; i < itemData.pantDatasList.Count; i++)
        {
            SetItem(pantItemHolder.transform, itemData.pantDatasList[i].pantSprite, itemData.pantDatasList[i].locked, i);
        }
    }

    public void SetItem(Transform holderTransform, Sprite sprite, bool locker, int index)
    {
        uiHolder = Instantiate(item);
        uiHolder.transform.SetParent(holderTransform);
        uiHolder.transform.localPosition = Vector3.zero;
        uiHolder.transform.localScale = new Vector3(1, 1, 1);

        uiHolder.GetComponent<Image>().sprite = sprite;

        getItemData = Cache.GetItemData(uiHolder);
        getItemData.itemData = index;
        getItemData.locker = locker;
    }
}
