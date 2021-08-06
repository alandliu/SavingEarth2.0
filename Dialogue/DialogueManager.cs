using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    [SerializeField]
    public Text dialogueText;
    public Text nameText;
    public int lettersPerSecond;
    public int soundsPerSecond;
    public int currentLine = 0;
    public Dialogue dialogue;
    public string eventName;
    public bool isDone;
    public bool isPlaying;



    public static DialogueManager Instance { get; private set; }
    public AudioManager am;

   private void Awake()
    {
        Instance = this;
        isDone = true;
        isPlaying = false;
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
        am.Play("dialog");
        StartCoroutine(TypeDialogue(dialogue.Lines[0], dialogue.Names[0], eventName));
        
        //GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().loadMG(eventName);
    }

    public IEnumerator TypeDialogue(string dialogue, string name, string eventName)
    {
        dialogueText.text = "";
        nameText.text = name;
        isDone = false;
        foreach (var letter in dialogue.ToCharArray())
        {
            if (dialogue.EndsWith("0"))
            {
                currentLine = 0;
                GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().loadMG(eventName);
                dialogueBox.SetActive(false);
                break;
            }
            dialogueText.text += letter;
            if (!isPlaying && !isDone)
            {
                StartCoroutine(playSound());
            }
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isDone = true;

    }

    public IEnumerator playSound()
    {
        isPlaying = true;
        am.Play("text");
        yield return new WaitForSeconds(1f / soundsPerSecond);
        isPlaying = false;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isDone)
        {
            ++currentLine;
            dialogueText.text = "";
            if (currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine], dialogue.Names[currentLine], eventName));
            }
            else
            {
                dialogueBox.SetActive(false);
                currentLine = 0;
                GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().state = GameManager.GameState.freeRoam;
            }
        } 
    }
}
