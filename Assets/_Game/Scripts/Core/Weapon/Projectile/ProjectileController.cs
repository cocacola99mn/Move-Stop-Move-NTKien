using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Character bulletShooter;

    public Projectile projectile;
    
    private float projecTileSpeed, projectileExistTime, existTime, scale;

    public Transform projectileTransform, projectileObjectTransform;

    public Vector3 direction;

    void Start()
    {
        OnInit();
    }

    void FixedUpdate()
    {
        TransformProjectile();
        ProjectileLifeTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnProjectileHit(other);
    }

    public virtual void OnInit()
    {
        projecTileSpeed = 6;
        existTime = 1.5f;
        
        projectileExistTime = existTime;
        
        scale = (bulletShooter.characterLevel - 1) * 0.1f;

        ProjectileScale(scale);
    }

    public void ProjectileLifeTime()
    {
        projectileExistTime -= Time.deltaTime;

        if (projectileExistTime <= 0)
        {
            DespawnProjectile();
        }                
    }

    public void OnProjectileHit(Collider other)
    {
        if (other.gameObject.CompareTag(GameConstant.DAMAGEABLE_TAG))
        {
            DespawnProjectile();

            bulletShooter.OnGetKill(Cache.GetCharacter(other));
        }
    }

    public virtual void TransformProjectile()
    {
        projectileObjectTransform.Translate(Vector3.forward * projecTileSpeed * Time.deltaTime);
    }

    public void ProjectileScale(float x)
    {
        projectileObjectTransform.localScale += new Vector3(x, x, x);
    }

    public virtual void DespawnProjectile()
    {
        ObjectPooling.Ins.Despawn(projectile.id + "", gameObject);

        projectileExistTime = existTime;
    }
}
