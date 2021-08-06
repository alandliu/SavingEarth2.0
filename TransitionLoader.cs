using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionLoader : MonoBehaviour
{

    public static TransitionLoader instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
