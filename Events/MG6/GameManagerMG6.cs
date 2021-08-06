using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMG6 : MonoBehaviour
{
    public GameObject Document;
    public Document currentDocument;
    public Text contentText;
    public Text accuracyText;

    public int correct;
    public int doneCount;
    public int threshold;

    public GameObject winScreen;
    public GameObject loseScreen;


    public DocContent[] contents;
    /*public string[] contents;
    public bool[] doneYet;
    public int[] docClasses;*/
    public DocContent curContent;
    public int curCorrectAnswer;

    public int randInt;

    // Start is called before the first frame update
    void Start()
    {
        newDocument();
        correct = 0;
    }

    /*public DocContent chooseContent()
    {
        randInt = Random.Range(0, contents.Length);
        while (!contents[randInt].checkDone())
        {
            randInt = Random.Range(0, contents.Length);
        }
        contents[randInt].setTrue();
        return contents[randInt];
    }
    */
    public void Left()
    {
        currentDocument.Accept();
    }

    public void Right()
    {
        currentDocument.Reject();
    }

    public void newDocument()
    {
        currentDocument = Instantiate(Document, transform.position, Quaternion.identity).GetComponent<Document>();
    }

    public void SetContents()
    {
        randInt = Random.Range(0, contents.Length);
        //while (!doneYet[randInt])
        while (contents[randInt].hasDone)
        {
            randInt = Random.Range(0, contents.Length);
        }
        contents[randInt].setTrue();
        curContent = contents[randInt];
        //doneYet[randInt] = true;
        //contentText.text = contents[randInt];
        //curCorrectAnswer = docClasses[randInt];
        contentText.text = curContent.contents;
        curCorrectAnswer = curContent.docClass;
    }

    /*public void setContents()
    {
        curContent = chooseContent();
        contentText.text = curContent.contents;
        curCorrectAnswer = curContent.docClass;
    }*/

    public void checkFinished()
    {
        doneCount = 0;
        foreach(DocContent dc in contents)
        {
            if (dc.hasDone) doneCount++;
        }
        if (doneCount == contents.Length)
        {
            checkWin();
        }
    }

    public void checkWin()
    {
        if (correct >= threshold)
        {
            Time.timeScale = 0f;
            winScreen.SetActive(true);
        } else
        {
            Time.timeScale = 0f;
            loseScreen.SetActive(true);
        }
    }

    public void setCorrect()
    {
        accuracyText.text = "Correct";
        correct++;
    }

    public void setIncorrect()
    {
        accuracyText.text = "Incorrect";
    }
}
