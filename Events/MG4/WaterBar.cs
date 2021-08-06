using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public int refillRate = 5;
    public int refillTick = 1;


    public void Start()
    {
        SetMaxHealth(100);
        StartCoroutine(refill());
    }

    public IEnumerator refill()
    {
        while (true)
        {
            yield return new WaitForSeconds(refillTick);
            setHealth(Mathf.Min(((int) slider.value + refillRate), 100));
        }
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

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
