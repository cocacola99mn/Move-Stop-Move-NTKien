using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSkinShop : UICanvas
{
    DataManager dataIns;
    ItemListData itemData;
    public SkinShopItem skinShopItem;

    public GameObject hatScroll, pantScroll, itemTouched, equipButton, priceButton;
    public Text skinDesText, priceText, coinText;

    public int itemIndex, goldNum;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        dataIns = DataManager.Ins;
        itemData = dataIns.itemListData;

        itemIndex = 0;
        coinText.text = dataIns.playerDataSO.Gold.ToString();
        OnItemClicked(false);
    }

    public void CloseButton()
    {
        dataIns.player.GetPant(dataIns.playerDataSO.Pant);
        dataIns.player.GetHat(dataIns.playerDataSO.Hat);

        Destroy(gameObject);

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
        OnItemClicked(false);
    }

    public void OnItemClicked(bool locker)
    {
        if (locker)
        {
            equipButton.SetActive(false);
            priceButton.SetActive(true);

        }
        else
        {
            equipButton.SetActive(true);
            priceButton.SetActive(false);
        }

        if (pantScroll.activeInHierarchy)
        {
            skinDesText.text = itemData.pantDatasList[itemIndex].skinDes;
            priceText.text = itemData.pantDatasList[itemIndex].price.ToString();
            dataIns.player.GetPant(itemIndex);
            
        }
        else
        {
            skinDesText.text = itemData.hatDatasList[itemIndex].skinDes;
            priceText.text = itemData.hatDatasList[itemIndex].price.ToString();
            dataIns.player.GetHat(itemIndex);
        }
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

    public void PriceButton()
    {
        
        if (pantScroll.activeInHierarchy)
        {
            dataIns.SetIntData(GameConstant.PREF_PANTEQUIP, ref dataIns.playerDataSO.Pant, itemIndex);
            CaculateGold(itemData.pantDatasList[itemIndex].price);
        }

        else
        {
            dataIns.SetIntData(GameConstant.PREF_HATEQUIP, ref dataIns.playerDataSO.Hat, itemIndex);
            CaculateGold(itemData.hatDatasList[itemIndex].price);
        }
    }

    public void CaculateGold(int price)
    {
        if (dataIns.playerDataSO.Gold >= price)
        {
            goldNum = dataIns.playerDataSO.Gold - price;
            coinText.text = goldNum.ToString();
            dataIns.SetIntData(GameConstant.PREF_GOLD, ref dataIns.playerDataSO.Gold, goldNum);
        }
    }

    public void ShopButton()
    {
        Debug.Log("Shop");
    }
}
