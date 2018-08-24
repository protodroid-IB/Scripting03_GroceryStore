using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;


// Script Name: GameController.cs
// Written By: Laurence Valentini

public class GameController : MonoBehaviour
{

    // references
    private FirstPersonController fpsController;
    private ScreenFader screenFader;
    private UIHandler handlerUI;
    private NarrativeDialogue narrativeDialogue;
    private GameObject objectiveDialogue;
    private DialogueController dialogueController;

    // the current objective state
    [SerializeField]
    private ObjectiveState currentObjectiveState = ObjectiveState.Start;

    // skip start boolean for testing
    [SerializeField]
    private bool skipStart = true;

    // a timer
    private float timer = 0f;

    // time before starting first mission
    private float startToFirstMissionTime = 0.5f;

    // the array of objective descriptions
    private string[] objectives;

    // is an npc talking
    private bool npcTalking = false;

    // are you talking to the manager
    private bool talkingToManager = false;

    // the managers door
    private Door managersDoor;
    
    // has the keycode been found
    private bool keycodeFound = false;

    // the number of digits of the keycode found
    private int digitsFound = 0;

    // the digits in the keycode found so far
    private int[] digits;

    // the digit found dialogue
    private bool digitFoundDialogue = false;

    // the found digit
    private int foundDigit = -1;


    private bool runCredits = false;

	// Use this for initialization
	void Start ()
    {
        // grab references
        screenFader = GameObject.FindWithTag("ScreenFader").GetComponent<ScreenFader>();
        narrativeDialogue = GetComponent<NarrativeDialogue>();
        fpsController = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        objectiveDialogue = GameObject.FindWithTag("ObjectiveDialogue");

        // set objective dialogue to false
        objectiveDialogue.SetActive(false);

        handlerUI = GetComponent<UIHandler>();

        // initialise objectives string array
        objectives = new string[3];


        // set the three objectives
        objectives[0] = "Find The Manager's Office!";
        objectives[1] = "Find The Keycode for the storeroom!";
        objectives[2] = "Complain to the Manager!";

        // initialise keycode digits array
        digits = new int[4];

        // cycle through each and make sure they equal -1
        for (int i = 0; i < digits.Length; i++)
        {
            digits[i] = -1;
        }

        // grab the dialogue controller
        dialogueController = GetComponent<DialogueController>();
    }
	


	// Update is called once per frame
	void Update ()
    {
        
        ObjectiveStateSwitch();
	}

    void ObjectiveStateSwitch()
    {
        // switch that handles which objective is current and the associated behaviour
        switch (currentObjectiveState)
        {
            case ObjectiveState.Start:
                StartObjective();
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

            case ObjectiveState.Finish:
                Finish();
                break;
        }
    }






    private void StartObjective()
    {
        // if not skipping the start
        if(skipStart == false)
        {
            // disable player controls
            fpsController.enabled = false;

            // if intro narrative is done and the screen has faded in
            if (narrativeDialogue.IntroNarrativeDone() && screenFader.FadeDone())
            {
                // if the timer until the start of the first mission is done
                if (Timer(ref timer, startToFirstMissionTime, false))
                {
                    // start the objective dialogue 
                    objectiveDialogue.SetActive(true);

                    // if objective dialogue is true, update it's UI
                    if (objectiveDialogue.activeInHierarchy == true)
                        handlerUI.UpdateObjectiveDialogue("OBJECTIVE UPDATED", objectives[0]);
                }
                   
                // if the timer has reached 4 seconds
                if (Timer(ref timer, 4f, true))
                {
                    // set objective dialogue to false change objective state and enable player controls
                    objectiveDialogue.SetActive(false);
                    currentObjectiveState = ObjectiveState.ManagerOffice;
                    fpsController.enabled = true;
                }
                
            }

            // start the intro narrative
            narrativeDialogue.StartIntroNarrative();
        }
        else
        {
            currentObjectiveState = ObjectiveState.ManagerOffice;
            fpsController.enabled = true;
        }
        
    }



    private void ManagersOffice()
    {
        // if the managers door has been found
        if (managersDoor.GetFoundDoor())
        {
            // disable the players controls
            fpsController.enabled = false;

            // activate the objective dialogue
            objectiveDialogue.SetActive(true);

            // update the objective UI
            if(objectiveDialogue.activeInHierarchy == true)
                handlerUI.UpdateObjectiveDialogue("OBJECTIVE UPDATED", objectives[1]);

            // once the objective UI has finished, disable it, change objective state and re-enable player controls
            if (Timer(ref timer, 4f, true))
            {
                objectiveDialogue.SetActive(false);
                currentObjectiveState = ObjectiveState.FindKey;
                fpsController.enabled = true;
            }

            
        }

        // update objective UI
        handlerUI.UpdateObjective(objectives[0]);
        
        // ensure screen fade is set to false
        screenFader.gameObject.SetActive(false);
    }




    
    private void FindKey()
    {
        // if the key has not been found
        if (keycodeFound == true)
        {
            // disable the players controls
            fpsController.enabled = false;

            // activate the objective dialogue
            objectiveDialogue.SetActive(true);

            // update the objective UI
            if (objectiveDialogue.activeInHierarchy == true)
                handlerUI.UpdateObjectiveDialogue("OBJECTIVE UPDATED", objectives[2]);

            // once the objective UI has finished, disable it, change objective state and re-enable player controls
            if (Timer(ref timer, 4f, true))
            {
                objectiveDialogue.SetActive(false);
                currentObjectiveState = ObjectiveState.KeyFound;
                fpsController.enabled = true;
            }
        }
        
        // if the digit found dialogue is true
        if(digitFoundDialogue == true)
        {
            // display digit found objective dialogue UI
            DigitFoundDialogue();
        }

        //update UI
        handlerUI.UpdateObjective(objectives[1]);
    }


    // if the key has been found
    private void KeyFound()
    {
        // update the objective
        handlerUI.UpdateObjective(objectives[2]);
        
        // make sure the managers door can be unlocked
        managersDoor.SetCanUnlock(true);

        // if the player has initiated conversation with the manager
        if(dialogueController.GetCurrentNPC().GetItemName().ToUpper() == "MANAGER")
        {
            talkingToManager = true;
        }

        // if the player has initiated conversation with the manager
        // update the obejctive, fade the screen to the outro and disable player controls
        if (talkingToManager == true)
        {
            currentObjectiveState = ObjectiveState.Finish;
            screenFader.gameObject.SetActive(true);
            narrativeDialogue.StartOutroNarrative();
            fpsController.enabled = false;
        }
    }


    private void Finish()
    {

        // if the outro narrative is done
        if(narrativeDialogue.OutroNarrativeDone())
        {
                fpsController.enabled = true;
                SceneManager.LoadScene("Credits");
        }
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


    public bool Timer(ref float inTimer, float inTime, bool resetAtCompletion)
    {
        bool done = false;

        if(inTimer >= inTime)
        {
            done = true;

            if (resetAtCompletion == true) inTimer = 0f;
        }
        else
        {
            inTimer += Time.deltaTime;
        }

        return done;
    }



    public GameObject GetObjectiveDialogue()
    {
        return objectiveDialogue;
    }



    private void DigitFoundDialogue()
    {
        fpsController.enabled = false;

        objectiveDialogue.SetActive(true);

        if (objectiveDialogue.activeInHierarchy == true)
            handlerUI.UpdateObjectiveDialogue("KEYCODE DIGIT FOUND!", "YOU GOT THE DIGIT " + foundDigit.ToString());

        if (Timer(ref timer, 4f, true))
        {
            objectiveDialogue.SetActive(false);
            DigitFound(foundDigit);
            fpsController.enabled = true;
            digitFoundDialogue = false;
        }
    } 


    public void SetDigitFound(int inDigit)
    {
        foundDigit = inDigit;
        digitFoundDialogue = true;
    }
}
