using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    public GameObject projectileEquipped;

    public bool isFiring;
    
    public float timeBetweenShots, shotCounter;

    public Transform firePoint;

    void Start()
    {
        timeBetweenShots = 3;
        isFiring = false;
    }

    void Update()
    {
        FiringProjectile();
    }

    public void FiringProjectile()
    {
        if (isFiring == true)
            ShotDelay();
        else
            shotCounter = 0.5f;
    }

    public void ShotDelay()
    {
        shotCounter -= Time.deltaTime;
        
        if (shotCounter <= 0)
        {
            shotCounter = timeBetweenShots;
            Instantiate(projectileEquipped, firePoint.position, firePoint.rotation);
        }
        else
            shotCounter -= Time.deltaTime;
    }
}
