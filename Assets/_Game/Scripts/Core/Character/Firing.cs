using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    public Character character;

    public GameObject weaponHolderObject, spawnedProjectile;

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
        if (isFiring)
        {
            ShotDelay(character.characterWeapon);
        }

        else
        {
            weaponHolderObject.SetActive(true);
            shotCounter = 0.8f;
        }
    }

    public virtual void ShotDelay(int weaponId)
    {
        shotCounter -= Time.deltaTime;
        
        if (shotCounter <= 0)
        {
            weaponHolderObject.SetActive(false);

            shotCounter = timeBetweenShots;

            spawnedProjectile = ObjectPooling.Ins.Spawn(weaponId + "", firePoint.position, firePoint.rotation);

            OnTypeSplit(weaponId);

            Cache.GetProjectileController(spawnedProjectile).bulletShooter = character;
        }

        else
        {
            shotCounter -= Time.deltaTime;
        }                                         
    }

    public void OnTypeSplit(int weaponId)
    {
        if (Cache.GetTypeSplit(spawnedProjectile) != null)
        {
            GameObject spawnedRight = ObjectPooling.Ins.Spawn(weaponId + "", firePoint.position, firePoint.rotation);
            GameObject spawnedLeft = ObjectPooling.Ins.Spawn(weaponId + "", firePoint.position, firePoint.rotation);

            Cache.GetProjectileController(spawnedRight).bulletShooter = character;
            Cache.GetProjectileController(spawnedLeft).bulletShooter = character;
            Cache.GetTypeSplit(spawnedRight).right = true;
            Cache.GetTypeSplit(spawnedLeft).left = true;
        }
    }
}
