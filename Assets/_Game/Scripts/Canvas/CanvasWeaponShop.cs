using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWeaponShop : UICanvas
{
    public List<GameObject> weapons;

    public WeaponHolder weaponHolder;

    public Transform weaponImageHolder;

    public Text weaponNameText, lockText,descriptionText;
    public GameObject nextButton, backButton;

    public int currentWeaponIndex;

    private void Start()
    {
        currentWeaponIndex = 0;
        backButton.SetActive(false);
        InitWeaponImage();
        GetWeaponShopData();
    }

    public void GetWeaponShopData()
    {
        OnReachShopBoundary();
        weapons[currentWeaponIndex].SetActive(true);
        weaponNameText.text = weaponHolder.weapons[currentWeaponIndex].weaponName;
        descriptionText.text = weaponHolder.weapons[currentWeaponIndex].description;
    }

    public void InitWeaponImage()
    {
        for (int i = 0; i < weaponHolder. weapons.Count; i++)
        {
            GameObject myWeapon = Instantiate(weaponHolder.weapons[i].weapon);
            
            myWeapon.SetActive(false);
            myWeapon.transform.SetParent(weaponImageHolder);
            myWeapon.transform.localPosition = new Vector3(0, 0, 0);
            myWeapon.transform.localScale += new Vector3(10, 10, 10);
            weapons.Add(myWeapon);
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

    public void EquipButton()
    {
        WeaponManager.Ins.OnChangeWeapon();
        WeaponManager.Ins.SetWeaponPref(weaponHolder.weapons[currentWeaponIndex].id);
        Debug.Log(WeaponManager.Ins.GetWeaponPref());
        WeaponManager.Ins.EquipWeapon();
    }

    public void NextButton()
    {
        weapons[currentWeaponIndex].SetActive(false);

        currentWeaponIndex++;

        GetWeaponShopData();
    }

    public void BackButton()
    {
        weapons[currentWeaponIndex].SetActive(false);

        currentWeaponIndex--;

        GetWeaponShopData();
    }

    public void OnReachShopBoundary()
    {
        if (currentWeaponIndex == weapons.Count - 1)
            nextButton.SetActive(false);
        else if (currentWeaponIndex == 0)
            backButton.SetActive(false);
        else
        {
            nextButton.SetActive(true);
            backButton.SetActive(true);
        }
    }
}
