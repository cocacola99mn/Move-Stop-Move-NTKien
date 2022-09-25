using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasFail : UICanvas
{
    Scene scene;
    public Text topText, killerText, goldText;

    public void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(scene.name);
    }
}
