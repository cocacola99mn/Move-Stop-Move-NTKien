using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSkinShop : UICanvas
{
    DataManager dataIns;
    ItemListData itemData;
    GetItemData getItemData;
    public SkinShopItem skinShopItem;

    public GameObject hatScroll, pantScroll, itemTouched, equipButton, priceButton, insufficentText;
    public Text skinDesText, priceText, coinText, equipText;

    public string noDes;
    public int itemIndex, goldNum;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        dataIns = DataManager.Ins;
        itemData = dataIns.itemListData;
        noDes = "No Description";
        itemIndex = 0;
        coinText.text = dataIns.playerDataSO.Gold.ToString();
        skinDesText.text = noDes;
        equipButton.SetActive(true);
        priceButton.SetActive(false);
    }

    public void CloseButton()
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        dataIns.player.GetPant(dataIns.playerDataSO.Pant);
        dataIns.player.GetHat(dataIns.playerDataSO.Hat);

        Destroy(gameObject);

        UIManager.Ins.OpenUI(UIID.UICMainMenu);
    }

   public void HatButton()
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        TopNavigator(hatScroll, pantScroll);
    }

    public void PantButton()
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        TopNavigator(pantScroll, hatScroll);
    }

    public void TopNavigator(GameObject activeScroll, GameObject deactiveScroll)
    {
        activeScroll.SetActive(true);
        deactiveScroll.SetActive(false);
        itemIndex = 0;
        skinDesText.text = noDes;
        equipButton.SetActive(true);
        priceButton.SetActive(false);
    }

    public void OnItemClicked(bool locker, GetItemData getItemData)
    {
        this.getItemData = getItemData;

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
            equipText.text = (itemIndex == dataIns.playerDataSO.Pant) ? GameConstant.EQUIPPED_TEXT : GameConstant.EQUIP_TEXT;
        }
        else
        {
            skinDesText.text = itemData.hatDatasList[itemIndex].skinDes;
            priceText.text = itemData.hatDatasList[itemIndex].price.ToString();
            dataIns.player.GetHat(itemIndex);
            equipText.text = (itemIndex == dataIns.playerDataSO.Hat) ? GameConstant.EQUIPPED_TEXT : GameConstant.EQUIP_TEXT;
        }

        insufficentText.SetActive(false);
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
    }

    public void EquipButton()
    {
        if (pantScroll.activeInHierarchy)
        {
            dataIns.SetIntData(GameConstant.PREF_PANTEQUIP, ref dataIns.playerDataSO.Pant, itemIndex);
            equipText.text = (itemIndex == dataIns.playerDataSO.Pant) ? GameConstant.EQUIPPED_TEXT : GameConstant.EQUIP_TEXT;
        }
        else
        {
            dataIns.SetIntData(GameConstant.PREF_HATEQUIP, ref dataIns.playerDataSO.Hat, itemIndex);
            equipText.text = (itemIndex == dataIns.playerDataSO.Hat) ? GameConstant.EQUIPPED_TEXT : GameConstant.EQUIP_TEXT;
        }

        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
    }

    public void PriceButton()
    {        
        if (pantScroll.activeInHierarchy)
        {            
            CaculateGold(itemData.pantDatasList[itemIndex].price, ref itemData.pantDatasList[itemIndex].locked);
            equipText.text = (itemIndex == dataIns.playerDataSO.Pant) ? GameConstant.EQUIPPED_TEXT : GameConstant.EQUIP_TEXT;
        }
        else
        {
            CaculateGold(itemData.hatDatasList[itemIndex].price, ref itemData.hatDatasList[itemIndex].locked);
            equipText.text = (itemIndex == dataIns.playerDataSO.Hat) ? GameConstant.EQUIPPED_TEXT : GameConstant.EQUIP_TEXT;
        }
    }

    public void OnBuySkin()
    {
        priceButton.SetActive(false);
        equipButton.SetActive(true);
    }

    public void CaculateGold(int price, ref bool locker)
    {
        if (dataIns.playerDataSO.Gold >= price)
        {
            goldNum = dataIns.playerDataSO.Gold - price;
            coinText.text = goldNum.ToString();
            dataIns.SetIntData(GameConstant.PREF_GOLD, ref dataIns.playerDataSO.Gold, goldNum);
            locker = false;
            getItemData.locker = false;
            getItemData.lockerObject.SetActive(false);
            EquipButton();
            OnBuySkin();
        }
        else
        {
            insufficentText.SetActive(true);
            AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        }
    }

    public void ShopButton()
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        Debug.Log("Shop");
    }
}
