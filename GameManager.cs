using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Due to plebian programming, I'll be using PlayerPrefs for data transfer between scenes

public class GameManager : MonoBehaviour
{

    
    private static GameManager instance;
    public Vector2 spawnPoint;
    public int curScene = 1;

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
        curScene++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Note MG scene indices must be higher

    }
}
