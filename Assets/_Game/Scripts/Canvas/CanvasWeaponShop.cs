using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWeaponShop : UICanvas
{
    public DataManager dataIns;

    public List<GameObject> weapons;

    public Transform weaponImageHolder;

    public Text weaponNameText, lockText, descriptionText;

    public GameObject nextButton, backButton;

    int currentWeaponIndex;

    Vector3 imageScale;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        dataIns = DataManager.Ins;

        currentWeaponIndex = 0;

        backButton.SetActive(false);

        imageScale = new Vector3(10, 10, 10);

        InitWeaponImage();

        GetWeaponShopData();
    }

    public void GetWeaponShopData()
    {
        OnReachShopBoundary();

        weapons[currentWeaponIndex].SetActive(true);

        weaponNameText.text = dataIns.itemListData.weaponList[currentWeaponIndex].weaponName;
        descriptionText.text = dataIns.itemListData.weaponList[currentWeaponIndex].description;
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
    }

    public void ChangePageButton(int index)
    {
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

        else if (currentWeaponIndex == 0)
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
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        Close();
    }

    public void ShopButon()
    {

    }
}
