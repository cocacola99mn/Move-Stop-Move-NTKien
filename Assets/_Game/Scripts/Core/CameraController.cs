using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] Transform Target, Camera;
    public GameObject attackRangeOutLine;
    float deltaZ;

    void Start()
    {
        deltaZ = Camera.position.z - Target.position.z;
    }

    void LateUpdate()
    {
        if(Target != null)
            Camera.position = new Vector3(Target.position.x , Camera.position.y , Target.position.z + deltaZ);
    }

    public void MoveToPlayer()
    {
        deltaZ += -7;

        Camera.localPosition = Vector3.Lerp(Camera.position, new Vector3(0, 10, -15), 1);
        Camera.localRotation = Quaternion.Euler(35, 0, 0);

        attackRangeOutLine.SetActive(true);
    }
}
