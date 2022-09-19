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

            OnTypeSplit(spawnedProjectile, weaponId);

            Cache.GetProjectileController(spawnedProjectile).bulletShooter = character;
        }
        else
            shotCounter -= Time.deltaTime;                                         
    }

    public void OnTypeSplit(GameObject gameObject, int weaponId)
    {
        float rotaionOffset = 30;
        Quaternion rightRotation = Quaternion.LookRotation(firePoint.position) * Quaternion.Euler(0, rotaionOffset, 0);
        Quaternion leftRotation = Quaternion.LookRotation(firePoint.position) * Quaternion.Euler(0, -rotaionOffset, 0);


        //TODO: test
        if (gameObject.GetComponent<TypeSplit>() != null)
        {
            GameObject spawnedRight = ObjectPooling.Ins.Spawn(weaponId + "", firePoint.position, rightRotation);
            GameObject spawnedLeft = ObjectPooling.Ins.Spawn(weaponId + "", firePoint.position, leftRotation);

            Cache.GetProjectileController(spawnedRight).bulletShooter = character;
            Cache.GetProjectileController(spawnedLeft).bulletShooter = character;
        }
    }
}
