using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasInfoBar : MonoBehaviour
{
    public Text nameText, pointText;

    public Image pointBarBackground;

    public Quaternion infoBarRotation;

    public Transform infoBarTransform;

    void Start()
    {
        OnInit();
    }

    void LateUpdate()
    {
        FreezeRotation();
    }

    public void OnInit()
    {
        infoBarRotation = Quaternion.Euler(0, 0, 0);
    }

    public void FreezeRotation()
    {
        infoBarTransform.rotation = infoBarRotation;
    }

    public void SetColor(Color32 color)
    {
        nameText.color = color;
        pointBarBackground.color = color;
    }
}
