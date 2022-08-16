using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform Target, Camera;
    float deltaZ;

    void Start()
    {
        deltaZ = Camera.position.z - Target.position.z;
    }

    void LateUpdate()
    {
        Camera.position = new Vector3(Target.position.x , Camera.position.y , Target.position.z + deltaZ);
    }
}
