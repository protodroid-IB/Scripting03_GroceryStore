using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private GameObject dialogueUI;

    private Text dialogue;

    private NPCDialogue currentTalkingNPC;

    private string[] currentDialogueArray;
    private string currentDialogue = "";

    private int dialogueIndex = 0;

    private bool dialogueStarted = false;

    private AudioSource dialogueProgressSound;

	// Use this for initialization
	void Start ()
    {
        dialogueUI = GameObject.FindWithTag("DialogueUI");
        dialogue = dialogueUI.transform.GetChild(0).GetChild(0).GetComponent<Text>();

        dialogueUI.SetActive(false);

        dialogueProgressSound = transform.GetChild(0).GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        ProgressDialogue();
	}


    private void LateUpdate()
    {
        if (dialogueUI.activeSelf == false)
        {
            dialogueStarted = false;
        }
    }



    public void StartDialogue(NPCDialogue npcDialogue, string[] inDialogue)
    {
        if (dialogueStarted == false) ProgressDialogueSound();

        dialogueUI.SetActive(true);

        dialogueStarted = true;

        currentTalkingNPC = npcDialogue;

        currentDialogueArray = inDialogue;

        currentDialogue = currentDialogueArray[dialogueIndex];

        dialogueIndex++;

        UpdateDialogue();
    }

    void UpdateDialogue()
    {
        dialogue.text = currentDialogue;
    }

    void ProgressDialogue()
    {
        if(Input.GetMouseButtonDown(0) && dialogueStarted)
        {
            if(dialogueIndex < currentDialogueArray.Length)
            {
                currentDialogue = currentDialogueArray[dialogueIndex];

                dialogueIndex++;

                UpdateDialogue();

                ProgressDialogueSound();
            }
            else
            {
                currentTalkingNPC.NotTalking();
                dialogueUI.SetActive(false);
               // dialogueStarted = false;
                dialogueIndex = 0;
            }
        }
    }


    public bool DialogueStarted()
    {
        return dialogueStarted;
    }

    private void ProgressDialogueSound()
    {
        dialogueProgressSound.Play();
    }

    public Item GetCurrentNPC()
    {
        return currentTalkingNPC.GetComponent<Item>();
    }
}
