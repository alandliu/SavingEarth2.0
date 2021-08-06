using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMG4 : MonoBehaviour
{

    public Text timer;
    public int maxSeconds;
    public int curSeconds;
    public bool ticking;
    public GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        curSeconds = maxSeconds;
        timer.text = "00:" + curSeconds;
        ticking = false;
        StartCoroutine(timerTake());
    }

    // Update is called once per frame
    void Update()
    {
        if (!ticking)
        {
            StartCoroutine(timerTake());
        }
    }

    private IEnumerator timerTake()
    {
        ticking = true;
        yield return new WaitForSeconds(1);
        curSeconds--;
        if (curSeconds <= 0)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
        }
        if (curSeconds >= 10) timer.text = "00:" + curSeconds;
        else timer.text = "00:0" + curSeconds;
        ticking = false;
    }
}
