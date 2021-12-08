using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance;
    private bool isDangerInRange = false;
    private Transform currentDanger;
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
    public bool IsDangerInRange
    {
        get => isDangerInRange;
        set => isDangerInRange = value;
    }

    public Transform CurrentDanger
    {
        get => currentDanger;
        set => currentDanger = value;
    }
}