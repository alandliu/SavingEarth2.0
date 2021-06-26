using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    [SerializeField]
    public Text dialogueText;
    public int lettersPerSecond;
    public int currentLine = 0;
    public Dialogue dialogue;
    public string eventName;
    public bool isDone;


    public static DialogueManager Instance { get; private set; }

   private void Awake()
    {
        Instance = this;
        isDone = true;
        /*if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }*/
    }

    public void showDialogue(Dialogue dialogue, string eventName)
    {
        this.dialogue = dialogue;
        this.eventName = eventName;
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0], eventName));
        
        //GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().loadMG(eventName);
    }

    public IEnumerator TypeDialogue(string dialogue, string eventName)
    {
        dialogueText.text = "";
        isDone = false;
        if (dialogue.EndsWith("0"))
        {
            currentLine = 0;
            GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().loadMG(eventName);
        }
        foreach (var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isDone = true;

    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isDone)
        {
            ++currentLine;
            dialogueText.text = "";
            if (currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine], eventName));
            }
            else
            {
                dialogueBox.SetActive(false);
                GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().state = GameManager.GameState.freeRoam;
            }
        } 
    }
}
