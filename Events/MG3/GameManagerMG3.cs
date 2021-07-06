using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMG3 : MonoBehaviour
{
    public int[] curBarHealths = new int[6];
    public int maxBarHealth = 100;
    public int randInt;
    public int curMouseVal;

    public HealthBar[] healthsBars = new HealthBar[6];
    public GameObject[] plots = new GameObject[6];


    private void Start()
    {
        curMouseVal = 0;
        for (int i = 0; i < curBarHealths.Length; i++)
        {
            curBarHealths[i] = maxBarHealth;
            healthsBars[i].SetMaxHealth(maxBarHealth);
        }
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }*/
    }

    public void TakeDamage(int damage, int index)
    {
        //randInt = Random.Range(0, 6);
        curBarHealths[index] = Mathf.Max(curBarHealths[index] - damage, 0);
        healthsBars[index].setHealth(curBarHealths[index]);
        if (curBarHealths[index] <= 0) GameManager.instance.returnLoss();
    }

    public void Heal(int heal, int index)
    {
        //randInt = Random.Range(0, 6);
        curBarHealths[index] = Mathf.Min(curBarHealths[index] + heal, 100);
        healthsBars[index].setHealth(curBarHealths[index]);
    }

    public void selectFertilizer()
    {
        curMouseVal = 0;
        Debug.Log("Fertilizer selected");
    }

    public void selectSeeds()
    {
        curMouseVal = 1;
        Debug.Log("Seeds selected");
    }

    public void selectWater()
    {
        curMouseVal = 2;
        Debug.Log("Water selected");
    }

    public void checkWin()
    {
        int numFinished = 0;
        for (int i = 0; i < plots.Length; i++)
        {
            if (plots[i].GetComponent<Plot>().curLevel == 3)
            {
                numFinished++;
            }
        }

        if (numFinished == 6)
        {
            Debug.Log("Win");
            GameManager.instance.returnWin();
        }
    }
}
