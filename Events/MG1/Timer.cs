using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 59;
    public bool countingDown = false;

    private void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    private void Update()
    {
        if (secondsLeft == 0)
        {
            GameManager.instance.returnWin();
        }
        if (countingDown == false && secondsLeft >= 0)
        {
            StartCoroutine(TimerTake());
        }
    }

    IEnumerator TimerTake()
    {
        countingDown = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft >= 10) textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        else textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        countingDown = false;
    }


}
