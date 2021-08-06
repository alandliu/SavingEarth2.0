using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public GameObject lossScreen;
    public Image fill;



    public void Start()
    {
        SetMaxHealth(100);
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void setHealth(int health)
    {
        slider.value = health;
        if (slider.value <= 0)
        {
            Time.timeScale = 0f;
            lossScreen.SetActive(true);
        }

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
