using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int maxVal;
    public int curVal;

    public Slider slider;

    public void setMax(int max)
    {
        maxVal = max;
        slider.maxValue = max;
        curVal = 0;
        setVal(curVal);
    }
    public void setVal(int val)
    {
        curVal = val;
        slider.value = curVal;
    }
}
