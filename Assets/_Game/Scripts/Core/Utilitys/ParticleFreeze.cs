using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFreeze : MonoBehaviour
{
    public Transform particleTransform;

    void Update()
    {
        FreezeRotation();
    }

    public void FreezeRotation()
    {
        particleTransform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
