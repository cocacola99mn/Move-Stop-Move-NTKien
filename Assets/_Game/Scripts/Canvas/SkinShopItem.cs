using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopItem : MonoBehaviour
{
    public CanvasSkinShop canvasSkinShop;
    public GetItemData getItemData, itemDataHolder;

    public GameObject hatItemHolder, pantItemHolder , itemBackGround, item3dHolder,  uiItem,uiHolder;
    public RectTransform uiHolderTransform;

    public List<Skin> skin;

    public int itemIndex;
    
    void Start()
    {
        GetAllSkin();
    }

    public void GetAllSkin()
    {
        for(int i = 0; i < skin.Count; i++)
        {
            switch (skin[i].skinType)
            {
                case SkinType.Hat:
                    
                    SetParentForItem(hatItemHolder.transform, i, (i == 8) ? new Vector3(0, 50, 0) : Vector3.zero);
                    
                    break;
                default:
                    SetParentForItem(pantItemHolder.transform, i, new Vector3(0, -140, 0));
                    break;
            }                       
        }
    }

    public void SetParentForItem(Transform holder, int elementArray, Vector3 position)
    {
        uiHolder = Instantiate(item3dHolder);
        uiHolder.transform.SetParent(holder);
        uiHolderTransform = uiHolder.GetComponent<RectTransform>();
        uiHolderTransform.localPosition = Vector3.zero;
        getItemData = uiHolder.GetComponent<GetItemData>();
        getItemData.itemData = elementArray;

        uiItem = Instantiate(skin[elementArray].skin);
        uiItem.transform.SetParent(uiHolder.transform);
        uiItem.transform.localPosition = position;
    }
}
