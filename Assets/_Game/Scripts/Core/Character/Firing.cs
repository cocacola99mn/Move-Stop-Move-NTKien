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

        character = GetComponent<Character>();
    }

    void Update()
    {
        FiringProjectile();
    }

    public void FiringProjectile()
    {
        if (isFiring)
            ShotDelay(character.playerWeapon);
        else
        {
            weaponHolderObject.SetActive(true);
            shotCounter = 0.8f;
        }
    }

    public void ShotDelay(int weaponId)
    {
        shotCounter -= Time.deltaTime;
        
        if (shotCounter <= 0)
        {
            weaponHolderObject.SetActive(false);
            shotCounter = timeBetweenShots;
            spawnedProjectile = ObjectPooling.Ins.Spawn(weaponId + "", firePoint.position, firePoint.rotation);

            Debug.Log(spawnedProjectile);

            Cache.GetProjectileController(spawnedProjectile).bulletShooter = character;

            //Error weapon Knife
        }
        else
            shotCounter -= Time.deltaTime;                                         
    }
}
