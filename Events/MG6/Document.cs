using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Document : MonoBehaviour
{

    public Animator currentAnim;
    public GameManagerMG6 gm;
    public bool canPress;
    // public Image im;


    // Start is called before the first frame update
    void Start()
    {
        currentAnim = GetComponent<Animator>();
        gm = FindObjectOfType<GameManagerMG6>();
        canPress = false;
        //StartCoroutine(loadContents());
    }


    public void Accept()
    {
        if (!canPress) return;
        currentAnim.SetTrigger("Left");
        if (gm.curCorrectAnswer == 0)
        {
            gm.setCorrect();
            Debug.Log("Correct");
        }
        else
        {
            gm.setIncorrect();
            Debug.Log("Incorrect");
        }
    }

    public void Reject()
    {
        if (!canPress) return;
        currentAnim.SetTrigger("Right");
        if (gm.curCorrectAnswer == 1)
        {
            gm.setCorrect();
            Debug.Log("Correct");
        }
        else
        {
            gm.setIncorrect();
            Debug.Log("Incorrect");
        }
    }

    public void replaceDoc()
    {
        gm.contentText.text = "";
        gm.newDocument();
        Destroy(this.gameObject);
    }

    /*public void setDoc()
    {
        gm.setContents();
    }*/
    public void canClick()
    {
        canPress = true;
        gm.SetContents();
    }

    /*IEnumerator loadContents()
    {
        yield return new WaitForSeconds(1f);
        //gm.SetContents();
    }*/
}
