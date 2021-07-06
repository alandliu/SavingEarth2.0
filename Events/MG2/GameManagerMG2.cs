using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManagerMG2 : MonoBehaviour
{
    public GameObject text;
    public int orbsLeft;
    public bool hasEnded;

    void Start()
    {
        text.GetComponent<Text>().text = "Orbs Left: " + orbsLeft;
        hasEnded = false;
        orbsLeft = 100;
    }

    public void checkEvery()
    {

    }
    public void updateScore(int orbsDestroyed)
    {
        if (orbsLeft >= 0) orbsLeft -= orbsDestroyed;
        text.GetComponent<Text>().text = "Orbs Left: " + orbsLeft;
        orbsLeft = Mathf.Max(0, orbsLeft);
        if (orbsLeft <= 0)
        {
            if (!hasEnded)
            {
                GameManager.instance.returnWin();
                hasEnded = true;
            }
        }
    }
}
