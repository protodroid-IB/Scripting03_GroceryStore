using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class NPCDialogue : MonoBehaviour
{
    private bool isTalking = false;
    private FirstPersonController fpsController;
    private GameController gameController;
    private DialogueController dialogueController;

    [SerializeField]
    private string[] dialogue;


    // Use this for initialization
    void Start ()
    {
        fpsController = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        dialogueController = GameObject.FindWithTag("GameController").GetComponent<DialogueController>();
    }
	

    public void Talk()
    {
        isTalking = true;
        Debug.Log("NPC TALK!");

        fpsController.enabled = false;
        gameController.SetNPCTalking(isTalking);

        dialogueController.StartDialogue(this, dialogue);
    }

    public void SetTalking(bool inTalking)
    {
        isTalking = inTalking;
    }

    public bool IsTalking()
    {
        return isTalking;
    }

    public void NotTalking()
    {
        isTalking = false;
        fpsController.enabled = true;
        gameController.SetNPCTalking(isTalking);
    }
}
