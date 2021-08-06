using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerMG5 : MonoBehaviour
{
    public GameManagerMG5 gm;
    public Text timer;
    public int maxSeconds;
    public int curSeconds;
    public bool countingDown;
    IEnumerator timing;

    private void Start()
    {
        gm = FindObjectOfType<GameManagerMG5>();
        maxSeconds = gm.spawners[gm.curBuilding].seconds;
        curSeconds = maxSeconds;
        if (curSeconds % 60 != 0) timer.text = "0" + curSeconds / 60 + ":" + curSeconds % 60;
        else timer.text = "0" + curSeconds / 60 + ":" + curSeconds % 60 + "0";
        countingDown = false;
        timing = timerTake();
        StartCoroutine(timing);
    }

    private void Update()
    {
        if (!countingDown)
        {
            timing = timerTake();
            StartCoroutine(timing);
        }
    }

    public IEnumerator timerTake()
    {
        countingDown = true;
        yield return new WaitForSeconds(1f);
        curSeconds -= 1;
        if (curSeconds % 60 < 10) {
            if (curSeconds % 60 != 0) timer.text = "0" + curSeconds / 60 + ":0" + curSeconds % 60;
            else timer.text = "0" + curSeconds / 60 + ":" + curSeconds % 60 + "0";
        }
        else timer.text = "0" + curSeconds / 60 + ":" + curSeconds % 60;

        if (curSeconds <= 0)
        {
            Time.timeScale = 0f;
            gm.lossScreen.SetActive(true);
        }
        countingDown = false;
    }

    public void switchTimers()
    {
        StopCoroutine(timing);
        maxSeconds = gm.spawners[gm.curBuilding].seconds;
        curSeconds = maxSeconds;
        if (curSeconds % 60 != 0) timer.text = "0" + curSeconds / 60 + ":" + curSeconds % 60;
        else timer.text = "0" + curSeconds / 60 + ":" + curSeconds % 60 + "0";
        StartCoroutine(timing);
    }
}
