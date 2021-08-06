using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMG3 : MonoBehaviour
{
    public int[] curBarHealths = new int[6];
    public Image[] buttons = new Image[3];
    public Sprite[] buttonImagesUS = new Sprite[3];
    public Sprite[] buttonImageS = new Sprite[3];
    public int maxBarHealth = 100;
    public int randInt;
    public int curMouseVal;

    public HealthBar[] healthsBars = new HealthBar[6];
    public GameObject[] plots = new GameObject[6];
    public GameObject winScreen;
    public GameObject loseScreen;


    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("BGMusic");
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
        if (curBarHealths[index] <= 0)
        {
            Time.timeScale = 0f;
            loseScreen.SetActive(true);
        }
    }

    public void Heal(int heal, int index)
    {
        //randInt = Random.Range(0, 6);
        curBarHealths[index] = Mathf.Min(curBarHealths[index] + heal, 100);
        healthsBars[index].setHealth(curBarHealths[index]);
    }

    public void selectFertilizer()
    {
        buttons[curMouseVal].sprite = buttonImagesUS[curMouseVal];
        curMouseVal = 0;
        buttons[curMouseVal].sprite = buttonImageS[curMouseVal];
        Debug.Log("Fertilizer selected");
    }

    public void selectSeeds()
    {
        buttons[curMouseVal].sprite = buttonImagesUS[curMouseVal];
        curMouseVal = 1;
        buttons[curMouseVal].sprite = buttonImageS[curMouseVal];
        Debug.Log("Seeds selected");
    }

    public void selectWater()
    {
        buttons[curMouseVal].sprite = buttonImagesUS[curMouseVal];
        curMouseVal = 2;
        buttons[curMouseVal].sprite = buttonImageS[curMouseVal];
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
            Time.timeScale = 0f;
            winScreen.SetActive(true);
        }
    }

    public void disableBar(int index)
    {
        healthsBars[index].gameObject.SetActive(false);
    }
}
