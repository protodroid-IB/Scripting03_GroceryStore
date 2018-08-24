using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script Name: DialogueController.cs
// Written By: Laurence Valentini

public class DialogueController : MonoBehaviour
{
    // dialogue GO and the text component
    private GameObject dialogueUI;
    private Text dialogue;

    // the currently talking NPC
    private NPCDialogue currentTalkingNPC;

    // the current array of dialogue strings
    private string[] currentDialogueArray;

    // the current dialogue
    private string currentDialogue = "";

    // the current dialogue index
    private int dialogueIndex = 0;

    // has the dialogue started?
    private bool dialogueStarted = false;

    // the sound played when dialogue progresses
    private AudioSource dialogueProgressSound;





	// Use this for initialization
	void Start ()
    {
        // grab the dialogue UI GO and its text component
        dialogueUI = GameObject.FindWithTag("DialogueUI");
        dialogue = dialogueUI.transform.GetChild(0).GetChild(0).GetComponent<Text>();

        // set the UI to false
        dialogueUI.SetActive(false);

        // grab the audio component
        dialogueProgressSound = transform.GetChild(0).GetComponent<AudioSource>();
    }
	


	// Update is called once per frame
	void Update ()
    {
        ProgressDialogue(); // progresses the dialogue
	}


    private void LateUpdate()
    {
        // in the late update if the dialogue UI is not active set dialogue started to false
        if (dialogueUI.activeSelf == false)
        {
            dialogueStarted = false;
        }
    }


    // this method initiates dialogye with an NPC
    public void StartDialogue(NPCDialogue npcDialogue, string[] inDialogue)
    {
        // if the dialogue has not yet started, play the dialogue sound
        if (dialogueStarted == false) ProgressDialogueSound();

        // activate the dialogue UI
        dialogueUI.SetActive(true);
        dialogueStarted = true;

        // set the currently talking npc
        currentTalkingNPC = npcDialogue;

        // set the array of strings for the dialogue
        currentDialogueArray = inDialogue;

        // set the current dialogue from the dialogue array
        currentDialogue = currentDialogueArray[dialogueIndex];

        // increment the index
        dialogueIndex++;

        // update the dialogue
        UpdateDialogue();
    }




    void UpdateDialogue()
    {
        // sets the text to the current dialogue
        dialogue.text = currentDialogue;
    }




    void ProgressDialogue()
    {
        // if the mouse button is pressed and the dialogue has started
        if(Input.GetMouseButtonDown(0) && dialogueStarted)
        {
            // if there is more dialogue
            if(dialogueIndex < currentDialogueArray.Length)
            {
                // set the new dialogue line
                currentDialogue = currentDialogueArray[dialogueIndex];

                // increment the index
                dialogueIndex++;

                // update the UI
                UpdateDialogue();

                // play the sound
                ProgressDialogueSound();
            }
            
            // if the dialogue has finished
            else
            {
                // set npc to not talking
                currentTalkingNPC.NotTalking();

                // deactivate the dialogue window
                dialogueUI.SetActive(false);

                // reset the index
                dialogueIndex = 0;
            }
        }
    }


    public bool DialogueStarted()
    {
        return dialogueStarted;
    }

    // this method plays the dialogue progress sound
    private void ProgressDialogueSound()
    {
        dialogueProgressSound.Play();
    }

    public Item GetCurrentNPC()
    {
        return currentTalkingNPC.GetComponent<Item>();
    }
}
