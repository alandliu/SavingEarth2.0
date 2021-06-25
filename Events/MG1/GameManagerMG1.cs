using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMG1 : MonoBehaviour
{

    public float speed = 2f;
    public static GameManagerMG1 instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

}
