using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWeaponShop : UICanvas
{
    DataManager dataIns;

    public List<Weapon> weaponList;

    public List<GameObject> weapons;

    public Transform weaponImageHolder;
    public GameObject nextButton, backButton, equipButtonObject, priceButtonObject;
    public Text weaponNameText, weaponPriceText ,lockText, descriptionText, coinText;

    int currentWeaponIndex, firstWeaponIndex, gold;

    Vector3 imageScale;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        dataIns = DataManager.Ins;
        weaponList = dataIns.itemListData.weaponList;
        currentWeaponIndex = 0;
        backButton.SetActive(false);
        imageScale = new Vector3(10, 10, 10);
        coinText.text = dataIns.playerDataSO.Gold.ToString();
        firstWeaponIndex = 0;

        InitWeaponImage();

        GetWeaponShopData();
    }

    public void GetWeaponShopData()
    {
        OnReachShopBoundary();

        weapons[currentWeaponIndex].SetActive(true);

        weaponNameText.text = weaponList[currentWeaponIndex].weaponName;
        descriptionText.text = weaponList[currentWeaponIndex].description;

        if (weaponList[currentWeaponIndex].locked)
        {
            priceButtonObject.SetActive(true);
            equipButtonObject.SetActive(false);
            weaponPriceText.text = weaponList[currentWeaponIndex].price.ToString();
        }
        else
        {
            priceButtonObject.SetActive(false);
            equipButtonObject.SetActive(true);
        }
    }

    public void InitWeaponImage()
    {
        for (int i = 0; i < dataIns.weaponObjectList.Count; i++)
        {
            Transform myWeapon = Instantiate(dataIns.weaponObjectList[i]).transform;

            myWeapon.gameObject.SetActive(false);
            myWeapon.SetParent(weaponImageHolder);
            myWeapon.localPosition = Vector3.zero;
            myWeapon.localScale += imageScale;

            weapons.Add(myWeapon.gameObject);
        }
    }

    public void EquipButton()
    {
        dataIns.SetIntData(GameConstant.PREF_WEAPONEQUIP, ref dataIns.playerDataSO.Weapon, currentWeaponIndex);
        dataIns.player.GetWeapon(currentWeaponIndex);
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
    }

    public void PriceButton()
    {
        if(dataIns.playerDataSO.Gold >= weaponList[currentWeaponIndex].price)
        {
            gold = dataIns.playerDataSO.Gold - weaponList[currentWeaponIndex].price;
            dataIns.SetIntData(GameConstant.PREF_GOLD, ref dataIns.playerDataSO.Gold, gold);
            coinText.text = dataIns.playerDataSO.Gold.ToString();
            weaponList[currentWeaponIndex].locked = false;
            priceButtonObject.SetActive(false);
            equipButtonObject.SetActive(true);
        }

        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
    }

    public void ChangePageButton(int index)
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);

        weapons[currentWeaponIndex].SetActive(false);

        currentWeaponIndex += index;

        GetWeaponShopData();
    }

    public void OnReachShopBoundary()
    {
        if (currentWeaponIndex == weapons.Count - 1)
        {
            nextButton.SetActive(false);
        }
        else if (currentWeaponIndex == firstWeaponIndex)
        {
            backButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
            backButton.SetActive(true);
        }
    }

    public void CloseButton()
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        Close();
    }

    public void ShopButon()
    {
        AudioManager.Ins.PlayAudio(AudioName.ButtonClick);
    }
}
