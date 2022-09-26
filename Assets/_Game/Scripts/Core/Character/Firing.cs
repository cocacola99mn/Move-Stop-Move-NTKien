using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    public Character character;
    public ProjectileController projectile, projectileHolder;

    public GameObject weaponHolderObject;
    public Transform firePoint;

    public bool isFiring;   
    public float timeBetweenShots, shotCounter;

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
            ShotDelay(character.projectile);
        }
        else
        {
            weaponHolderObject.SetActive(true);
            shotCounter = 0.35f;
        }
    }

    public virtual void ShotDelay(ProjectileController projectile)
    {
        if (shotCounter <= 0)
        {
            weaponHolderObject.SetActive(false);

            shotCounter = timeBetweenShots;

            projectileHolder = SimplePool.Spawn<ProjectileController>(projectile, firePoint.position, firePoint.rotation);

            OnTypeSplit(projectile);

            projectileHolder.bulletShooter = character;
        }
        else
        {
            shotCounter -= Time.deltaTime;
        }
    }

    public void OnTypeSplit(ProjectileController projectile)
    {
        if (Cache.GetTypeSplit(projectileHolder.gameObject) != null)
        {
            ProjectileController spawnedRight = SimplePool.Spawn<ProjectileController>(projectile, firePoint.position, firePoint.rotation);
            ProjectileController spawnedLeft = SimplePool.Spawn<ProjectileController>(projectile, firePoint.position, firePoint.rotation);

            spawnedRight.bulletShooter = spawnedLeft.bulletShooter = character;

            Cache.GetTypeSplit(spawnedRight.gameObject).right = true;
            Cache.GetTypeSplit(spawnedLeft.gameObject).left = true;
        }
    }
}
