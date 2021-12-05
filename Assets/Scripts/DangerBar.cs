using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DangerBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private float time;

    /*private void Start()
    {
        time = 0;
        slider.value = time;
    }

    private void Update()
    {
        time += Time.deltaTime;
        slider.value = time;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }*/


    public void SetInitialTime(float time)
    {
        slider.maxValue = time;
        slider.value = 0;
    }
    public void SetTime(float time) {
        slider.value = time;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }   
}
