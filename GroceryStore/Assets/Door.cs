using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameController gameController;

    private DoorState currentState = DoorState.Locked;

    private GameObject doorFrameL, doorFrameR;

    [SerializeField]
    private bool canUnlock = false;

    private bool foundDoor = false;

    private Animator animDoorL, animDoorR;

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gameController.SetManagersDoor(this);

        doorFrameL = transform.GetChild(0).gameObject;
        doorFrameR = transform.GetChild(1).gameObject;

        animDoorL = transform.GetChild(0).GetComponent<Animator>();
        animDoorR = transform.GetChild(1).GetComponent<Animator>();
    }

    public void UnlockDoor()
    {
        if(currentState == DoorState.Locked)
        {
            currentState = DoorState.Unlocked;
        }

        Debug.Log("TRIGGERED!");

        animDoorL.SetTrigger("OpenDoor");
        animDoorR.SetTrigger("OpenDoor");

        transform.GetComponent<Item>().SetInteract(false);
        
    }

    public void LockDoor()
    {
        if (currentState == DoorState.Unlocked)
        {
            currentState = DoorState.Locked;
        }

        doorFrameL.SetActive(true);
        doorFrameR.SetActive(true);
    }



    public void TryToOpen()
    {
        if(canUnlock == true)
        {
            UnlockDoor();
        }

        if(canUnlock == false && foundDoor == false)
        {
            foundDoor = true;
        }

    }


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
