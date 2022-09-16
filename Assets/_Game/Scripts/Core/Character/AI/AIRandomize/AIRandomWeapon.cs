using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRandomWeapon : MonoBehaviour
{
    public AIController aIController;

    public List<GameObject> weaponList;

    public GameObject aiWeapon;

    public int weaponIndex;
    
    void Start()
    {
        getRandomWeapon();
    }

    public void getRandomWeapon()
    {
        weaponIndex = Random.Range(0, weaponList.Count - 1);

        aiWeapon = Instantiate(weaponList[weaponIndex]);
        aiWeapon.transform.SetParent(transform);
        aiWeapon.transform.localPosition = Vector3.zero;

        aIController.randomWeaponIndex = weaponIndex;
    }
}
