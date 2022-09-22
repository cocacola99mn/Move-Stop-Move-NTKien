using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopItem : MonoBehaviour
{
    public DataManager dataIns;

    public CanvasSkinShop canvasSkinShop;

    public GetItemData getItemData, itemDataHolder;

    public GameObject hatItemHolder, pantItemHolder , itemBackGround, item3dHolder,  uiItem, uiHolder;

    public RectTransform uiHolderTransform;

    Vector3 itemPos;

    public int itemIndex;
    
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        dataIns = DataManager.Ins;
        itemPos = new Vector3(0, -140, 0);

        GetItem(dataIns.hatObjectList, ref hatItemHolder, Vector3.zero);
        GetItem(dataIns.pantObjectList, ref pantItemHolder, itemPos);
    }

    public void GetItem(List<GameObject> itemList, ref GameObject itemHolder, Vector3 position)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            SetParentForItem(itemList ,itemHolder.transform, i, position);
        }
    }

    public void SetParentForItem(List<GameObject> itemList , Transform holder, int index, Vector3 position)
    {
        uiHolder = Instantiate(item3dHolder);
        uiHolder.transform.SetParent(holder);
        uiHolderTransform = Cache.GetRectTransform(uiHolder);
        uiHolderTransform.localPosition = Vector3.zero;

        getItemData = Cache.GetItemData(uiHolder);
        getItemData.itemData = index;

        uiItem = Instantiate(itemList[index]);
        uiItem.transform.SetParent(uiHolder.transform);
        uiItem.transform.localPosition = position;
    }
}
