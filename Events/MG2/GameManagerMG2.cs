using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManagerMG2 : MonoBehaviour
{
    public GameObject text;
    public SpriteRenderer sr;
    public Sprite[] conditions = new Sprite[3];
    public int orbsLeft;
    public bool hasEnded;

    public GameObject WinScreen;

    void Start()
    {
        sr.sprite = conditions[0];
        text.GetComponent<Text>().text = "" + orbsLeft;
        hasEnded = false;
        orbsLeft = 100;

        FindObjectOfType<AudioManager>().Play("BGMusic");
    }

    public void checkEvery()
    {

    }
    public void updateScore(int orbsDestroyed)
    {
        if (orbsLeft >= 0) orbsLeft = Mathf.Max(0, orbsLeft - orbsDestroyed);
        text.GetComponent<Text>().text = "" + orbsLeft;
        //orbsLeft = Mathf.Max(0, orbsLeft - orbsDestroyed);
        if (orbsLeft <= 0)
        {
            if (!hasEnded)
            {
                //GameManager.instance.returnWin();
                Time.timeScale = 0f;
                WinScreen.SetActive(true);
                hasEnded = true;
            }
        }
        if (orbsLeft <= 70)
        {
            sr.sprite = conditions[1];
        }
        if (orbsLeft <= 30)
        {
            sr.sprite = conditions[2];
        }
    }
}
