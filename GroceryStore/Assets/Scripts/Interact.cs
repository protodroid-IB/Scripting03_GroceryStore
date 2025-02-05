﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script Name: Interact.cs
// Written By: Laurence Valentini

public class Interact : MonoBehaviour
{
    private Camera mainCam;
    private GameController gameController;
    private DialogueController dialogueController;
    private GameObject dialogueUI;
    private NarrativeDialogue narrativeDialogue;

    [SerializeField]
    private float interactRange = 20f;

    private GameObject gameObjectHit;

    private GameObject emptyGO = null;

    private Vector3 rayDirectionPoint;

    [SerializeField]
    private bool debugMode = false;


    private GameObject itemInfoUI;
    private InventoryController inventory;


    // Use this for initialization
    void Awake ()
    {
        mainCam = Camera.main;
        itemInfoUI = GameObject.FindWithTag("ItemInfoUI");
        inventory = GameObject.FindWithTag("GameController").GetComponent<InventoryController>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        narrativeDialogue = GameObject.FindWithTag("GameController").GetComponent<NarrativeDialogue>();
        dialogueController = GameObject.FindWithTag("GameController").GetComponent<DialogueController>();
        dialogueUI = GameObject.FindWithTag("DialogueUI").gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        LookAt();

        if(Input.GetKeyDown(KeyCode.C))
        {
            inventory.ClearInventory();
        }
	}



    public void LookAt()
    {
        Vector3 rayOrigin = mainCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, mainCam.transform.forward, out hit, interactRange))
        {
            rayDirectionPoint = hit.point;
            gameObjectHit = hit.transform.gameObject;

            Interactable interactable = gameObjectHit.GetComponent<Interactable>();

            if(interactable != null)
            {
                if (interactable.GetInteractable())
                {
                    if(!gameController.GetNPCTalking() && !gameController.GetObjectiveDialogue().activeInHierarchy) itemInfoUI.SetActive(true);

                    if (gameController.GetObjectiveDialogue().activeInHierarchy) itemInfoUI.SetActive(false);
                    interactable.Hover();

                    if (Input.GetMouseButtonDown(0) && !dialogueController.DialogueStarted() && !gameController.GetObjectiveDialogue().activeInHierarchy && !narrativeDialogue.GetOutroNarrativeStart())
                    {
                        Item newItem = interactable.GetInteractedItem();

                        switch (newItem.GetItemType())
                        {
                            case ItemType.Stock:
                                inventory.AddItem(newItem);
                                break;

                            case ItemType.Door:
                                newItem.GetComponent<Door>().TryToOpen();
                                itemInfoUI.SetActive(false);
                                break;

                            case ItemType.NPC:
                                newItem.GetComponent<NPCDialogue>().Talk();
                                itemInfoUI.SetActive(false);
                                break;

                            case ItemType.Freezer:
                                newItem.GetComponent<Freezer>().Interact();
                                break;
                        }

                    }
                }
            }
            else
            {
                itemInfoUI.SetActive(false);
            }
        }
        else
        {
            rayDirectionPoint = rayOrigin + (mainCam.transform.forward * interactRange);
            gameObjectHit = emptyGO;
            itemInfoUI.SetActive(false);
        }


        if (debugMode == true)
        {
            Debug.Log("HIT: " + gameObjectHit);
        }
    }



    public GameObject GetGameObjectHit()
    {
        return gameObjectHit;
    }
}
