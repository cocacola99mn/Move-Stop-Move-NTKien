using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    public PlayerController playerController;
    public AIController aIController;

    public GameObject projectileEquipped, weaponHolderObject;

    public bool isFiring;
    
    public float timeBetweenShots, shotCounter;

    public Transform firePoint;

    void Start()
    {
        timeBetweenShots = 3;

        isFiring = false;

        if (GetComponent<AIController>() != null)
            aIController = GetComponent<AIController>();
    }

    void Update()
    {
        FiringProjectile();
    }

    public void FiringProjectile()
    {
        if (LevelManager.Ins.levelStarter)
        {
            if (isFiring)
            {
                if (GetComponent<AIController>() == null)
                    ShotDelay(playerController.playerWeapon);
                else if(aIController.isDead == false)
                    ShotDelay(aIController.randomWeaponIndex);
            }
            else
            {
                weaponHolderObject.SetActive(true);
                shotCounter = 0.8f;
            }
        }        
    }

    public void ShotDelay(int weaponId)
    {
        shotCounter -= Time.deltaTime;
        
        if (shotCounter <= 0)
        {
            weaponHolderObject.SetActive(false);
            shotCounter = timeBetweenShots;
            ObjectPooling.Ins.Spawn(weaponId + "", firePoint.position, firePoint.rotation);
        }
        else
            shotCounter -= Time.deltaTime;                                         
    }
}
