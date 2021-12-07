using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Endscreen : MonoBehaviour
{

    public TMP_Text textAlive;
    public TMP_Text textDanger;
    // Start is called before the first frame update

    public void Start()
    {
        SetLifeTime(DDOL.timeAlive);
        SetDangerTime(DDOL.timeInDanger);
    }

    public void SetLifeTime(float time)
    {
        textAlive.text = "Baby lived " + Mathf.Floor(time) + "s";
    }

    public void SetDangerTime(float time)
    {
        textDanger.text = "He had been in danger for " + Mathf.Floor(time) + "s";
    }
}
