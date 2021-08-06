using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject progressBar;
    //public GameObject textDisplay;
    public int maxSeconds = 59;
    public int secondsLeft = 59;
    public bool countingDown = false;
    public GameObject winScreen;

    private void Start()
    {
        maxSeconds = secondsLeft;
        //textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        progressBar.GetComponent<ProgressBar>().setMax(secondsLeft);
    }

    private void Update()
    {
        if (secondsLeft == 0)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
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
        //if (secondsLeft >= 10) textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        //else textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        if (secondsLeft <= 3)
        {
            progressBar.GetComponent<ProgressBar>().setVal(maxSeconds);
        }
        else if ((secondsLeft + 1) % 5 == 0)
        {
            progressBar.GetComponent<ProgressBar>().setVal(maxSeconds - secondsLeft);
        }
        countingDown = false;
    }


}
