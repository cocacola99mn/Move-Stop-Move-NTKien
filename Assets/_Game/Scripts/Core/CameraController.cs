using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] Transform Target, Camera;
    public GameObject attackRangeOutLine;
    public Vector3  yOffset, zOffset, cameraFollow;

    void Start()
    {
        OnInit();
    }

    void LateUpdate()
    {
        FollowPlayer();
    }

    public void OnInit()
    {
        GetZOffset();

        yOffset.y = 12;
    }

    public void GetZOffset()
    {
        zOffset.z = Camera.position.z - Target.position.z;
    }

    public void FollowPlayer()
    {
        if(Target != null)
        {
            cameraFollow.x = Target.position.x;
            cameraFollow.y = Camera.position.y;
            cameraFollow.z = Target.position.z + zOffset.z;

            Camera.position = cameraFollow;
        }
    }

    public void MoveToPlayer()
    {
        zOffset.z += -7;

        Camera.localPosition = Vector3.Lerp(Camera.position, yOffset, 1);
        Camera.localRotation = Quaternion.Euler(35, 0, 0);

        attackRangeOutLine.SetActive(true);
    }
}
