using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Save : MonoBehaviour
    {
        public static float bestTimeAlive;
        public static float bestScore;

    private void Start()
    {
        LoadGameData();
    }
    public static void SaveGameData()
        {
            if(GameManager.timeAlive > bestTimeAlive)  PlayerPrefs.SetFloat("bestTimeAlive", Mathf.Floor(GameManager.timeAlive));
            if(GameManager.ScoreCalculation() > bestScore)  PlayerPrefs.SetFloat("bestScore", GameManager.ScoreCalculation());
        }

        public static void LoadGameData()
    {
        bestTimeAlive = PlayerPrefs.GetFloat("bestTimeAlive");
        bestScore = PlayerPrefs.GetFloat("bestScore");
    }
    }

