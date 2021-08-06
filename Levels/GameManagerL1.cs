using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerL1 : MonoBehaviour
{
    public GameObject[] task0;
    public GameObject[] task1;
    public GameObject[] task2;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        if (GameManager.instance.playerTask == 0)
        {
            activate(task0);
        } else if (GameManager.instance.playerTask == 1)
        {
            activate(task1);
        } else if (GameManager.instance.playerTask == 2 || GameManager.instance.playerTask == 3)
        {
            activate(task2);
        }
    }

    void activate(GameObject[] objects)
    {
        foreach(GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }
}
