using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : GameUnit
{
    public Character bulletShooter;

    public Transform projectileTransform, projectileObjectTransform;

    public Vector3 direction, normalScale;
    [SerializeField]
    private float projecTileSpeed, projectileExistTime, existTime, scale;
    public bool boost;

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
        normalScale = new Vector3(1, 1, 1);
        projectileExistTime = existTime;
        
        if(bulletShooter != null)
        {
            scale = (bulletShooter.characterLevel - 1) * 0.1f;
        }

        if (boost)
        {
            projecTileSpeed = 8;
        }

        ProjectileScaleUp(scale);
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
            if (!boost)
            {
                DespawnProjectile();
            }

            bulletShooter.OnGetKill(Cache.GetCharacter(other));
        }
    }

    public virtual void TransformProjectile()
    {
        projectileObjectTransform.Translate(Vector3.forward * projecTileSpeed * Time.deltaTime);
        
        if (boost)
        {
            ProjectileScaleUp(0.015f);
        }
    }

    public void ProjectileScaleUp(float x)
    {
        projectileObjectTransform.localScale += new Vector3(x, x, x);
    }

    public virtual void DespawnProjectile()
    {
        if (boost)
        {
            projectileObjectTransform.localScale = normalScale;
            boost = false;
            projecTileSpeed = 6;
        }

        SimplePool.Despawn(this);

        projectileExistTime = existTime;
    }
}
