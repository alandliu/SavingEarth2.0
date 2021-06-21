using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class placeholder : MonoBehaviour
{
    public GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    public void win()
    {
        gm.returnWin();
    }

    public void loss()
    {
        gm.returnLoss();
    }
}
