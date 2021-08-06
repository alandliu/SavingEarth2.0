using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMG1 : MonoBehaviour
{

    public float speed;
    public int maxTime;
    public int time;
    public int divider = 5;
    public static GameManagerMG1 instance { get; private set; }

    private void Awake()
    {
        instance = this;
        maxTime = FindObjectOfType<Timer>().secondsLeft;
        time = maxTime;
        speed = 2f;
        FindObjectOfType<AudioManager>().Play("BGMusic");
    }

    private void Update()
    {
        updateDifficulty();
    }

    public void updateDifficulty()
    {
        time = FindObjectOfType<Timer>().secondsLeft;
        if (maxTime - time != 0)
        {
            speed = 2f + (float)((maxTime - time) / divider);
            FindObjectOfType<Obstacle>().cdTime = 10f - (float)((maxTime - time) / 8);
        }
    }

}
