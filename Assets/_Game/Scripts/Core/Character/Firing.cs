using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    public PlayerController playerController;
    public AIController aIController;

    public GameObject projectileEquipped;

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
        if (isFiring)
            ShotDelay();
        else
            shotCounter = 0.8f;
    }

    public void ShotDelay()
    {
        shotCounter -= Time.deltaTime;
        
        if (shotCounter <= 0)
        {
            shotCounter = timeBetweenShots;
            ObjectPooling.Ins.Spawn(WeaponManager.Ins.GetWeaponPref() + "", firePoint.position, firePoint.rotation);
        }
        else
        {
            shotCounter -= Time.deltaTime;
        }                           
    }
}
