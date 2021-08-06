using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMG5 : MonoBehaviour
{

    public int curBuilding;
    public int maxHealth;
    public int health;
    public bool grounded;
    public GameObject lossScreen;
    public GameObject winScreen;
    public TimerMG5 timer;

    public LayerSpawner[] spawners;

    [HideInInspector]
    public BuildingLayer currentLayer;

    void Start()
    {
        grounded = false;
        curBuilding = 0;
        spawners[curBuilding].gameObject.SetActive(true);
        spawners[curBuilding].SpawnLayer();
        health = maxHealth;
    }


    private void Update()
    {
        detectInput();
    }
    public void detectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentLayer.DropLayer();
        }
    }

    public void spawnNewLayer()
    {
        Invoke("newLayer", 2f);
    }

    public void spawnNewLayer(BuildingLayer bl)
    {
        newLayer(bl);
    }
    
    void newLayer()
    {
        spawners[curBuilding].SpawnLayer();
    }

    void newLayer(BuildingLayer bl)
    {
        spawners[curBuilding].SpawnSpecific(bl);
    }

    public void loseHealth()
    {
        health--;
        if (health <= 0)
        {
            lossScreen.SetActive(true);
        } else
        {
            spawnNewLayer();
        }
    }

    public void switchBuildings()
    {
        spawners[curBuilding].gameObject.SetActive(false);
        curBuilding++;
        timer.switchTimers();
        if (curBuilding >= 3)
        {
            winScreen.SetActive(true);
            return;
        }
        spawners[curBuilding].gameObject.SetActive(true);
        grounded = false;
    }
}
