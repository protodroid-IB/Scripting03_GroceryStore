using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script Name: NPCMission.cs
// Written By: Laurence Valentini


public class NPCMission : MonoBehaviour
{
    private GameController gameController;

    private NPCDialogue npcDialogue;

    private InventoryController inventory;

    [SerializeField]
    private NPCMissionState currentMissionState = NPCMissionState.None;

    [System.Serializable]
    public struct Dialogue
    {
        public string[] none;
        public string[] hasMission;
        public string[] missionStarted;
        public string[] missionFinished;
        public string[] afterMission;
    }

    private bool dialogueSet = false;

    private bool missionComplete = false;

    [SerializeField]
    private Dialogue dialogue;

    [SerializeField]
    private Item[] itemsRequired;

    private List<Item> itemsPickedUp = new List<Item>();

    private bool keyRewarded = false;

	// Use this for initialization
	void Start ()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        inventory = GameObject.FindWithTag("GameController").GetComponent<InventoryController>();
        npcDialogue = GetComponent<NPCDialogue>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameController.GetObjectiveState() == ObjectiveState.FindKey || gameController.GetObjectiveState() == ObjectiveState.KeyFound)
        {
            switch (currentMissionState)
            {
                case NPCMissionState.None:
                    SetNoneDialogue();
                    break;

                case NPCMissionState.HasMission:
                    SetHasMissionDialogue();
                    HasMission();
                    break;

                case NPCMissionState.MissionStarted:
                    SetMissionStartedDialogue();
                    MissionStarted();
                    break;

                case NPCMissionState.MissionFinished:
                    SetMissionFinishedDialogue();
                    MissionFinished();
                    break;

                case NPCMissionState.AfterMission:
                    AfterMission();
                    SetAfterMissionDialogue();
                    break;

                default:
                    SetNoneDialogue();
                    break;
            }
        }
	}




    private void HasMission()
    {
        if(npcDialogue.GetHasSpoken())
        {
            currentMissionState = NPCMissionState.MissionStarted;
            npcDialogue.SetHasSpoken(false);
        }
    }

    private void MissionStarted()
    {
        if(missionComplete == true)
        {
            currentMissionState = NPCMissionState.MissionFinished;
        }
    }

    private void MissionFinished()
    {
        if(npcDialogue.GetHasSpoken())
        {
            currentMissionState = NPCMissionState.AfterMission;
            npcDialogue.SetHasSpoken(false);
            inventory.ClearInventory();
        }
    }

    private void AfterMission()
    {
        if(npcDialogue.IsTalking() == false)
        {
            if (keyRewarded == false)
            {
                gameController.SetDigitFound(Random.Range(0, 10));
                keyRewarded = true;
            }
        }
    }




    private void SetNoneDialogue()
    {
        npcDialogue.SetDialogue(dialogue.none, currentMissionState);
    }

    private void SetHasMissionDialogue()
    {
        npcDialogue.SetDialogue(dialogue.hasMission, currentMissionState);
    }

    private void SetMissionStartedDialogue()
    {
        npcDialogue.SetDialogue(dialogue.missionStarted, currentMissionState);
    }

    private void SetMissionFinishedDialogue()
    {
        npcDialogue.SetDialogue(dialogue.missionFinished, currentMissionState);
    }

    private void SetAfterMissionDialogue()
    {
        npcDialogue.SetDialogue(dialogue.afterMission, currentMissionState);
    }



    public bool CheckMissionComplete()
    {
        int equalCount = 0;

        for (int i = 0; i < inventory.GetInventoryArray().Length; i++)
        {
            for(int j = 0; j < itemsRequired.Length; j++)
            {
                if(inventory.GetInventoryArray()[i].Equals(itemsRequired[j]))
                {
                    if(itemsPickedUp.Count > 0)
                    {
                        for (int k = 0; k < itemsPickedUp.Count; k++)
                        {
                            if(!inventory.GetInventoryArray()[i].Equals(itemsPickedUp[k]))
                            {
                                equalCount++;
                                itemsPickedUp.Add(inventory.GetInventoryArray()[i]);
                            }
                        }
                    }
                    else
                    {
                        equalCount++;
                        itemsPickedUp.Add(inventory.GetInventoryArray()[i]);
                    }
                }

                if (equalCount >= 3)
                {
                    missionComplete = true;
                    itemsPickedUp.Clear();
                    return missionComplete;
                }
            }
        }

        itemsPickedUp.Clear();

        return missionComplete;
    }


    public string[] GetMissionCompleteDialogue()
    {
        return dialogue.missionFinished;
    }

}
