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

    private NPCMission npcMission;
    private NPCMissionState currentNPCMissionState = NPCMissionState.NotSet;

    private bool hasSpoken = false;


    // Use this for initialization
    void Start ()
    {
        fpsController = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        dialogueController = GameObject.FindWithTag("GameController").GetComponent<DialogueController>();
        npcMission = GetComponent<NPCMission>();
    }
	

    public void Talk()
    {
        isTalking = true;

        fpsController.enabled = false;
        gameController.SetNPCTalking(isTalking);

        if(currentNPCMissionState == NPCMissionState.MissionStarted)
        {
            if(npcMission.CheckMissionComplete())
            {
                dialogue = npcMission.GetMissionCompleteDialogue();
            }
        }

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
        hasSpoken = true;
    }

    public void SetDialogue(string[] inDialogueArray, NPCMissionState inState)
    {
        if(currentNPCMissionState != inState)
        {
            dialogue = inDialogueArray;
        }

        currentNPCMissionState = inState;
    }

    public bool GetHasSpoken()
    {
        return hasSpoken;
    }

    public void SetHasSpoken(bool inBool)
    {
        hasSpoken = inBool;
    }


}
