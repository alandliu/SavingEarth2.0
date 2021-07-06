using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plot : MonoBehaviour
{
    public int num;
    public int upgradeDone;
    public int curDone;
    public int curLevel;
    public int nextNeed;
    public int damagePerInstance;
    public bool isInNeed;
    public bool countingDown;
    public int randNeed;

    public GameManagerMG3 gm;
    public GameObject thoughtBubble;
    public GameObject[] Needs = new GameObject[3];
    public Sprite[] levelImages = new Sprite[4];
    public SpriteRenderer sr;


    private void Start()
    {
        gm = FindObjectOfType<GameManagerMG3>();
        sr = GetComponent<SpriteRenderer>();
        curDone = 0;
        curLevel = 0;
        damagePerInstance = 5;
        isInNeed = false;
        calcNextNeed();
        thoughtBubble.SetActive(false);
        for (int i = 0; i < Needs.Length; i++) Needs[i].SetActive(false);
        sr.sprite = levelImages[curLevel];


    }


    private void Update()
    {

        if (curLevel == 3) return;
        if (!countingDown)
        {
            if (isInNeed)
            {
                StartCoroutine(NeedTimerTake());
                return;
            }

            if (nextNeed > 0)
            {
                StartCoroutine(TimerTake());
            }
            else if (nextNeed == 0)
            {
                activateNeed();
                isInNeed = true;
            }
        }
    }


    public void calcNextNeed()
    {
        nextNeed = Random.Range(5, 16);
    }

    public void activateNeed()
    {
        randNeed = Random.Range(0, 3);
        thoughtBubble.SetActive(true);
        Needs[randNeed].SetActive(true);
    }

    public void clicked()
    {
        Debug.Log("Clicked");
        if (isInNeed)
        {
            if (randNeed == gm.curMouseVal)
            {
                isInNeed = false;
                gm.Heal(20, num);
                curDone++;
                thoughtBubble.SetActive(false);
                Needs[randNeed].SetActive(false);
                if (curDone >= upgradeDone)
                {
                    curDone = 0;
                    curLevel++;
                    sr.sprite = levelImages[curLevel];
                    gm.checkWin();
                }
                countingDown = false;
                damagePerInstance += 2;
                StopAllCoroutines();
                calcNextNeed();
            }
        } else
        {
            return;
        }
    }

    IEnumerator TimerTake()
    {
        countingDown = true;
        yield return new WaitForSeconds(1);
        nextNeed -= 1;
        countingDown = false;
    }

    IEnumerator NeedTimerTake()
    {
        countingDown = true;
        yield return new WaitForSeconds(1);
        gm.TakeDamage(damagePerInstance, num);
        countingDown = false;
    }

}
