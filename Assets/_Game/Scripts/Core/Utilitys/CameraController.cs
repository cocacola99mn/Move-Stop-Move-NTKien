using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public Camera mainCamera;
    [SerializeField] Transform Target, Camera;
    public GameObject attackRangeOutLine;
    public Vector3  yPos,yOffset, zOffset, moveFurtherOffset ,cameraFollow;

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
        zOffset.z = Camera.position.z - Target.position.z; // Get ZOffset

        yOffset.y = 0;
        yPos.y = 15;
        moveFurtherOffset = new Vector3(0, 2, 2);
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
        zOffset.z += -5;

        Camera.localPosition = Vector3.Lerp(Camera.position, yPos, 1);
        Camera.localRotation = Quaternion.Euler(45, 0, 0);

        attackRangeOutLine.SetActive(true);
    }

    public void MoveFurtherFromPlayer()
    {
        Camera.localPosition += moveFurtherOffset;
        zOffset.z -= 2;
    }
}
