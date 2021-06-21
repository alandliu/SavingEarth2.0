using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour, Interactable
{

    [SerializeField] Dialogue dialogue;
    public string mgName;
    public int mgNum;
    public bool hasPlayed = false;

    public void Interact()
    {
        DialogueManager.Instance.showDialogue(dialogue, "");
        GameManager.instance.state = GameManager.GameState.Frozen;
    }
    public void Interact(string eventName)
    {
        DialogueManager.Instance.showDialogue(dialogue, eventName);
        GameManager.instance.state = GameManager.GameState.Frozen;
        
    }

}
