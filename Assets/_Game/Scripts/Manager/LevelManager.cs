using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public GameObject UIGameplay;
    
    public Text UIAliveDisplayNumber;
    
    public int aliveNumber;

    public bool levelStarter;

    void Start()
    {
        aliveNumber = 100;
        SetAliveNumber();
    }

    public void SetAliveNumber()
    {
        UIAliveDisplayNumber.text = aliveNumber.ToString();
    }
}
