using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    public DialogueTrigger dialogueObject;
    void Start()
    {
        dialogueObject.TriggerDialogue();
    }
}
