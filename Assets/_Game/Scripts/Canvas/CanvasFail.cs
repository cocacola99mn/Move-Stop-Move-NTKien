using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFail : UICanvas
{
    public Text topText, killerText, goldText;

    public void ContinueButton()
    {
        GameManager.Ins.Restart();
    }
}
