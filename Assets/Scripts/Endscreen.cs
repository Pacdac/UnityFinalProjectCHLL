using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Endscreen : MonoBehaviour
{

    public TMP_Text textAlive;
    public TMP_Text textDanger;
    public TMP_Text textInVision;
    public TMP_Text textScore;
    public TMP_Text textBestScore;
    // Start is called before the first frame update

    public void Start()
    {
        Save.SaveGameData();
        SetLifeTime(GameManager.timeAlive);
        SetDangerTime(GameManager.timeInDanger);
        SetVisionTime(GameManager.timeInVision);
        SetScore(GameManager.ScoreCalculation());
        SetBestScore(Save.bestScore);
    }

    public void SetLifeTime(float time)
    {
        textAlive.text = "Baby lived " + Mathf.Floor(time) + "s";
    }

    public void SetDangerTime(float time)
    {
        textDanger.text = "He had been in danger for " + Mathf.Floor(time) + "s";
    }

    public void SetVisionTime(float time)
    {
        textInVision.text = "You had kept an eye on him for " + Mathf.Floor(time) + "s";
    }

    public void SetScore(float score)
    {
        textScore.text = "Your score is " + Mathf.Floor(score);
    }

    public void SetBestScore(float bestScore)
    {
        textBestScore.text = "Your best score is " + Mathf.Floor(bestScore);
    }
}
