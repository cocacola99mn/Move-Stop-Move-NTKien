using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSkinShop : UICanvas
{
    public List<Skin> skinListCanvas;

    public GameObject hatScroll, pantScroll,itemTouched;
    
    public SkinShopItem skinShopItem;

    public Text skinDesText;

    public int itemIndex;

    private void Start()
    {
        itemIndex = 0;
        OnItemClicked();
    }

    public void CloseButton()
    {
        SkinManager.Ins.EquipHat();
        SkinManager.Ins.EquipPant();
        Close();
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
    }

    public void HatButton()
    {
        pantScroll.SetActive(false);
        hatScroll.SetActive(true);
        itemIndex = 1;
        OnItemClicked();
    }

    public void PantButton()
    {
        pantScroll.SetActive(true);
        hatScroll.SetActive(false);
        itemIndex = 0;
        OnItemClicked();
    }

    public void OnItemClicked()
    {
        skinDesText.text = skinShopItem.skin[itemIndex].skinDes;
        
        switch (skinShopItem.skin[itemIndex].skinType)
        {
            case SkinType.Hat:
                SkinManager.Ins.TryHat(skinShopItem.skin[itemIndex].skinId - 1);
                break;
            default:
                SkinManager.Ins.TryPant(skinShopItem.skin[itemIndex].skinId - 1);
                break;
        }
    }

    public void EquipButton()
    {
        switch (skinShopItem.skin[itemIndex].skinType)
        {
            case SkinType.Hat:
                SkinManager.Ins.DeactiveHat();
                SkinManager.Ins.SetHatPref(skinShopItem.skin[itemIndex].skinId);
                SkinManager.Ins.EquipHat();
                break;
            default:
                SkinManager.Ins.SetPantPref(skinShopItem.skin[itemIndex].skinId);
                SkinManager.Ins.EquipPant();
                break;
        }
    }

    public void ShopButton()
    {

    }
}
