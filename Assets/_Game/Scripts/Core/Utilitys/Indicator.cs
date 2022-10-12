using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : GameUnit
{
    public Text pointText;

    public Image pointBackground, arrowImage;

    public Transform arrowTransform, indicatorTransform;

    public Camera mainCamera;

    public Character character;

    private Vector3 viewPortPos, viewPortOffset;
    
    private float xMax, yMax, indicatorCircleR, screenOffset;
    private float x, y, z, px, py, pz, ratio;
    private float posX, posY, rad, deg, zeroFloat;

    public void Awake()
    {
        OnInit();
    }

    private void Update()
    {
        IndicateEnemy();
    }

    public void OnInit()
    {
        screenOffset = 75;
        xMax = Screen.width / 2 - screenOffset;
        yMax = Screen.height / 2 - screenOffset;

        indicatorCircleR = 50;

        zeroFloat = 0.001f;

        viewPortOffset = new Vector3(0.5f, 0.5f, 0);

        mainCamera = CameraController.Ins.mainCamera;
    }

    public void AttachCharacter(Character character)
    {
        this.character = character;
        indicatorTransform.localPosition = Vector3.zero;
    }

    private void SetActive(bool isActive)
    {
        pointBackground.gameObject.SetActive(isActive);
        arrowImage.gameObject.SetActive(isActive);
    }

    private bool TargetInScreen()
    {
        viewPortPos = mainCamera.WorldToViewportPoint(character.characterTransform.position);

        if ((viewPortPos.x > 0 && viewPortPos.x < 1) && (viewPortPos.y > 0 && viewPortPos.y < 1))
        {
            SetActive(false);
            return true;
        }

        viewPortPos -= viewPortOffset;
        SetActive(true);

        return false;
    }

    private void IndicateEnemy()
    {
        if (!TargetInScreen())
        {
            UpdateIndicatorPos();
        }
    }

    //Update Indicator Position
    private void UpdateIndicatorPos()
    {
        x = viewPortPos.x;
        y = viewPortPos.y;
        z = viewPortPos.z;

        pz = 1f;

        if (Mathf.Abs(z) > zeroFloat)
        {
            pz = z / Mathf.Abs(z);
        }

        if (Mathf.Abs(x) <= zeroFloat)
        {
            posX = 0f;
            posY = yMax * y / Mathf.Abs(y) * pz;
        }

        if (Mathf.Abs(y) <= zeroFloat)
        {
            posY = 0f;
            posX = xMax * x / Mathf.Abs(x) * pz;
        }

        px = x / Mathf.Abs(x);
        py = y / Mathf.Abs(y);
        ratio = Mathf.Abs(x / y);

        if (ratio > 1)
        {
            posX = xMax * px * pz;
            posY = yMax / ratio * py * pz;
        }
        else
        {
            posY = yMax * py * pz;
            posX = xMax * ratio * px * pz;
        }

        indicatorTransform.localPosition = new Vector3(posX, posY, 0f);

        //Update Arrow Position
        rad = Mathf.Atan2(y * pz, x * pz);
        deg = rad * Mathf.Rad2Deg;
        arrowTransform.rotation = Quaternion.Euler(0f, 0f, deg - 90f);
        arrowTransform.localPosition = new Vector3(indicatorCircleR * Mathf.Cos(rad), indicatorCircleR * Mathf.Sin(rad), 0f);
    }

    public void DespawnIndicator()
    {
        SimplePool.Despawn(this);
    }

    public void SetIndicatorPoint(int point)
    {
        pointText.text = point.ToString();
    }

    public void SetIndicatorColor(Color color)
    {
        pointBackground.color = color;
        arrowImage.color = color;
    }
}
