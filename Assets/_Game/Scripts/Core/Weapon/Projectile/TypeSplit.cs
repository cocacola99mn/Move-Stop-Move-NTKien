using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeSplit : ProjectileController
{
    public bool left, right;
    Vector3 angle;
    public override void OnInit()
    {
        base.OnInit();

        angle = new Vector3(0, 15, 0);

        GetRotateOffset();  
    }

    public override void DespawnProjectile()
    {
        base.DespawnProjectile();

        left = right = false;
    }

    public void GetRotateOffset()
    {
        if (right)
        {
            projectileObjectTransform.eulerAngles += angle;
        }

        if(left)
        {
            projectileObjectTransform.eulerAngles -= angle;
        }
    }

}
