using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DocContent
{
    public string contents;
    // public Image im;
    public int docClass;

    public bool hasDone = false;

    public void setTrue()
    {
        hasDone = true;
    }

    public bool checkDone()
    {
        return hasDone;
    }
}
