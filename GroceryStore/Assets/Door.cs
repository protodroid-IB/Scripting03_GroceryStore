using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private DoorState currentState = DoorState.Locked;

    private GameObject doorFrameL, doorFrameR;

    [SerializeField]
    private bool canUnlock = true;


    private void Start()
    {
        doorFrameL = transform.GetChild(0).gameObject;
        doorFrameR = transform.GetChild(1).gameObject;
    }

    public void UnlockDoor()
    {
        if(currentState == DoorState.Locked)
        {
            currentState = DoorState.Unlocked;
        }

        doorFrameL.SetActive(false);
        doorFrameR.SetActive(false);

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
    }


    public void SetCanUnlock(bool inActive)
    {
        canUnlock = inActive;
    }
}
