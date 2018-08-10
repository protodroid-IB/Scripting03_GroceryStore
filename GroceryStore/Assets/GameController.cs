using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private UIHandler handlerUI;

    [SerializeField]
    private ObjectiveState currentObjectiveState = ObjectiveState.Start;

    private string[] objectives;

    private bool npcTalking = false;

    private Door managersDoor;

    private bool keycodeFound = false;
    private int digitsFound = 0;
    private int[] digits;

	// Use this for initialization
	void Start ()
    {
        handlerUI = GetComponent<UIHandler>();

        objectives = new string[3];

        objectives[0] = "Find The Manager's Office!";
        objectives[1] = "Find The Keycode for the storeroom!";
        objectives[2] = "Complain to the Manager!";

        digits = new int[4];

        for (int i = 0; i < digits.Length; i++)
        {
            digits[i] = -1;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        ObjectiveStateSwitch();
	}

    void ObjectiveStateSwitch()
    {
        switch(currentObjectiveState)
        {
            case ObjectiveState.Start:
                // implement start of game!!!!
                currentObjectiveState = ObjectiveState.ManagerOffice;
                break;

            case ObjectiveState.ManagerOffice:
                ManagersOffice();
                break;

            case ObjectiveState.FindKey:
                FindKey();
                break;

            case ObjectiveState.KeyFound:
                KeyFound();
                break;
        }
    }


    private void ManagersOffice()
    {
        if(managersDoor.GetFoundDoor())
        {
            currentObjectiveState = ObjectiveState.FindKey;
        }

        handlerUI.UpdateObjective(objectives[0]);
    }


    private void FindKey()
    {
        if (keycodeFound == true)
        {
            //managersDoor.SetCanUnlock(true);
            currentObjectiveState = ObjectiveState.KeyFound;
        }

        handlerUI.UpdateObjective(objectives[1]);
    }


    private void KeyFound()
    {
        handlerUI.UpdateObjective(objectives[2]);
        managersDoor.SetCanUnlock(true);
    }











    public ObjectiveState GetObjectiveState()
    {
        return currentObjectiveState;
    }


    public void SetNPCTalking(bool inBool)
    {
        npcTalking = inBool;
    }

    public bool GetNPCTalking()
    {
        return npcTalking;
    }


    public void SetManagersDoor(Door inDoor)
    {
        managersDoor = inDoor;
    }

    public void DigitFound(int inDigit)
    {
        bool set = false;

        for(int i=0; i < digits.Length; i++)
        {
            if(digits[i] == -1 && set == false)
            {
                digits[i] = inDigit;
                digitsFound++;
                set = true;
            }

            Debug.Log(digits[i].ToString());
        }

        CheckAllDigitsFound();
        handlerUI.UpdateKeyCodeDigits(digits);
    }

    public void CheckAllDigitsFound()
    {
        if(digitsFound >= 4)
        {
            keycodeFound = true;
        }
    }
}
