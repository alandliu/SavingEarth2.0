using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLossButtons : MonoBehaviour
{
    

    public void pressLose()
    {
        GameManager.instance.returnLoss();
    }

    public void pressWin()
    {
        GameManager.instance.returnWin();
    }

}
