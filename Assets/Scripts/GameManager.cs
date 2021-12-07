using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance;
    private bool isFacingDanger = false;

    private GameManager()
    {
    }

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
        }
        return _instance;
    }
    public bool IsFacingDanger
    {
        get => isFacingDanger;
        set => isFacingDanger = value;
    }

}