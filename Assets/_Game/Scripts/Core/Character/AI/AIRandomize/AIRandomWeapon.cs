using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRandomWeapon : MonoBehaviour
{
    public AIController aIController;

    public GameObject aiWeapon;

    public int weaponIndex;
    
    void Start()
    {
        getRandomWeapon();
    }

    public void getRandomWeapon()
    {
        weaponIndex = Random.Range(0, DataManager.Ins.weaponObjectList.Count - 1);

        aiWeapon = Instantiate(DataManager.Ins.weaponObjectList[weaponIndex]);
        aiWeapon.transform.SetParent(transform);
        aiWeapon.transform.localPosition = Vector3.zero;

        aIController.playerWeapon = weaponIndex;
    }
}
