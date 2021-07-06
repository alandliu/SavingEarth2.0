using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    
    public static GameManager instance;
    public Vector2 spawnPoint;
    public int curScene = 1;
    public bool isRunning = true;  // for dialogue ig
    public bool spawned = false;

    
    public int playerTask = 0;
    public int playerHealth = 3;
    public Vector2 playerpos;


    public enum GameState { freeRoam, Frozen };
    public GameState state = GameState.freeRoam;
    

    private void Awake()
    { 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } else {
            Destroy(gameObject);
        }
    }


    public void nextLevel()
    {
        Debug.Log("Next Level");
        spawned = false;
        playerTask = 0;
        state = GameState.freeRoam;
        curScene++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Note MG scene indices must be higher
    }

   
    public void loadMG(string mgName)
    {
        SceneManager.LoadScene(mgName);
        state = GameState.Frozen;
        //transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
    }

    public void returnWin()
    {
        playerTask++;
        state = GameState.freeRoam;
        SceneManager.LoadScene(curScene);
        //transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
    }

    public void returnLoss()
    {
        playerHealth--;
        state = GameState.freeRoam;
        SceneManager.LoadScene(curScene);
        //transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
    }

    public void Reset()
    {
        isRunning = true;
        playerTask = 0;
        playerHealth = 3;
        spawned = false;
        state = GameState.freeRoam;
    }
}
