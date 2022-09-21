using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSkinShop : UICanvas
{
    public DataManager dataIns;

    public GameObject hatScroll, pantScroll,itemTouched;
    
    public SkinShopItem skinShopItem;

    public Text skinDesText;

    public int itemIndex;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        dataIns = DataManager.Ins;

        itemIndex = 0;
    }

    public void CloseButton()
    {
        dataIns.player.GetPant(dataIns.playerDataSO.Pant);
        dataIns.player.GetHat(dataIns.playerDataSO.Hat);

        Close();

        UIManager.Ins.OpenUI(UIID.UICMainMenu);
    }

   public void HatButton()
    {
        TopNavigator(hatScroll, pantScroll);
    }

    public void PantButton()
    {
        TopNavigator(pantScroll, hatScroll);
    }

    public void TopNavigator(GameObject activeScroll, GameObject deactiveScroll)
    {
        activeScroll.SetActive(true);
        deactiveScroll.SetActive(false);
        itemIndex = 0;
        OnItemClicked();
    }

    public void OnItemClicked()
    {
        if (pantScroll.activeInHierarchy)
        {
            GetItemData(dataIns.itemListData.pantDatasList[itemIndex].skinDes);
            dataIns.player.GetPant(itemIndex);
        }

        else
        {
            GetItemData(dataIns.itemListData.hatDatasList[itemIndex].skinDes);
            dataIns.player.GetHat(itemIndex);
        }
    }

    public void GetItemData(string skinDes)
    {
        skinDesText.text = skinDes;
    }

    public void EquipButton()
    {
        if (pantScroll.activeInHierarchy)
        {
            dataIns.SetIntData(GameConstant.PREF_PANTEQUIP, ref dataIns.playerDataSO.Pant, itemIndex);
        }

        else
        {
            dataIns.SetIntData(GameConstant.PREF_HATEQUIP, ref dataIns.playerDataSO.Hat, itemIndex);
        }
    }

    public void ShopButton()
    {

    }
}
