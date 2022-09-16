using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Projectile projectile;
    
    public float projecTileSpeed, projectileExistTime, corpseExistTime;

    public Transform weapon;

    void Start()
    {
        projecTileSpeed = 5;
        projectileExistTime = 2;
        corpseExistTime = 2;
    }

    void Update()
    {
        GetProjectileType();

        ProjectileLifeTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnProjectileHit(other);
    }

    public void ProjectileLifeTime()
    {
        projectileExistTime -= Time.deltaTime;

        if (projectileExistTime <= 0)
        {
            DespawnProjectile();

            projectileExistTime = 2;
        }                
    }

    public void OnProjectileHit(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstant.DAMAGEABLE_TAG))
        {
            DespawnProjectile();

            projectileExistTime = 2;
        }
    }

    public void DespawnProjectile()
    {
        ObjectPooling.Ins.Despawn(projectile.id + "", gameObject);
    }

    public void MovingProjectile()
    {
        transform.Translate(Vector3.forward * projecTileSpeed * Time.deltaTime);
    }

    public void TypeSpin()
    {
        weapon.Rotate(0, 0, 10, Space.Self);
    }

    public void GetProjectileType()
    {
        switch (projectile.weaponType)
        {
            case WeaponType.Spin:
                MovingProjectile();
                TypeSpin();
                break;
            case WeaponType.Split:
                MovingProjectile();
                break;
            default:
                //Type: Normal
                MovingProjectile();
                break;
        }
    }

}
