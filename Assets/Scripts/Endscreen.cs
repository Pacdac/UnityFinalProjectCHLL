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
    // Start is called before the first frame update

    public void Start()
    {
        SetLifeTime(GameManager.timeAlive);
        SetDangerTime(GameManager.timeInDanger);
        SetVisionTime(GameManager.timeInVision);
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
}
