using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script Name: Door.cs
// Written By: Laurence Valentini

public class Door : MonoBehaviour
{
    private GameController gameController;

    // the current state of the door
    private DoorState currentState = DoorState.Locked;

    // each door gameobject (assumes it's a double door)
    private GameObject doorFrameL, doorFrameR;

    // can the door be unlocked
    [SerializeField]
    private bool canUnlock = false;

    // has the door been found
    private bool foundDoor = false;

    // the animators for both doors
    private Animator animDoorL, animDoorR;

    // is this door the managers door
    [SerializeField]
    private bool isManagersDoor = false;

    // the open door sound
    private AudioSource openDoorSound;



    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        // if its the managers door tell the gamecontroller
        if(isManagersDoor) gameController.SetManagersDoor(this);

        // set both door frame GOs
        doorFrameL = transform.GetChild(0).gameObject;
        doorFrameR = transform.GetChild(1).gameObject;

        // grab each doors animators
        animDoorL = transform.GetChild(0).GetComponent<Animator>();
        animDoorR = transform.GetChild(1).GetComponent<Animator>();

        // grab the audio
        openDoorSound = GetComponent<AudioSource>();
    }




    public void UnlockDoor()
    {
        // if the current state of the door is locked, set it to unlocked
        if(currentState == DoorState.Locked)
        {
            currentState = DoorState.Unlocked;
        }

        // open both doors
        animDoorL.SetTrigger("OpenDoor");
        animDoorR.SetTrigger("OpenDoor");

        // play the sound
        openDoorSound.Play();

        // make doors non interactable
        transform.GetComponent<Item>().SetInteract(false);
        
    }







    public void TryToOpen()
    {
        // if the door can be unlocked
        if(canUnlock == true)
        {
            // unlock the door
            UnlockDoor();
        }

        // if the door can't be unlocked and the door hasn't been found yet and this method is called, it has now been found
        if(canUnlock == false && foundDoor == false)
        {
            foundDoor = true;
        }

    }

    // can this door be unlocked?
    public void SetCanUnlock(bool inActive)
    {
        canUnlock = inActive;
    }

    public bool GetCanUnlock()
    {
        return canUnlock;
    }

    public bool GetFoundDoor()
    {
        return foundDoor;
    }
}
