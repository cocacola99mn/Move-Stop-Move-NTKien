using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    Scene scene;

    protected void Awake()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        scene = SceneManager.GetActiveScene();

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        
        if (Screen.currentResolution.height > maxScreenHeight)
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);

        UIManager.Ins.OpenUI(UIID.UICMainMenu);        
    }

    public void Restart()
    {
        SceneManager.LoadScene(scene.name);
        SimplePool.ReleaseAll();
    }
}
