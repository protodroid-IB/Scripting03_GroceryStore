using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour
{
    private FirstPersonController fpsController;
    private ScreenFader screenFader;
    private UIHandler handlerUI;
    private NarrativeDialogue narrativeDialogue;
    private GameObject objectiveDialogue;

    [SerializeField]
    private ObjectiveState currentObjectiveState = ObjectiveState.Start;

    [SerializeField]
    private bool skipStart = true;

    private float timer = 0f;
    private float startToFirstMissionTime = 0.5f;

    private string[] objectives;

    private bool npcTalking = false;

    private Door managersDoor;

    private bool keycodeFound = false;
    private int digitsFound = 0;
    private int[] digits;
    private bool digitFoundDialogue = false;
    private int foundDigit = -1;

	// Use this for initialization
	void Start ()
    {
        screenFader = GameObject.FindWithTag("ScreenFader").GetComponent<ScreenFader>();
        narrativeDialogue = GetComponent<NarrativeDialogue>();
        fpsController = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        objectiveDialogue = GameObject.FindWithTag("ObjectiveDialogue");

        objectiveDialogue.SetActive(false);

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
        }
    }



    private void StartObjective()
    {
        if(skipStart == false)
        {
            fpsController.enabled = false;

            if (narrativeDialogue.IntroNarrativeDone() && screenFader.FadeDone())
            {
                if (Timer(ref timer, startToFirstMissionTime, false))
                {
                    objectiveDialogue.SetActive(true);

                    if (objectiveDialogue.activeInHierarchy == true)
                        handlerUI.UpdateObjectiveDialogue("OBJECTIVE UPDATED", objectives[0]);
                }
                   

                if (Timer(ref timer, 4f, true))
                {
                    objectiveDialogue.SetActive(false);
                    currentObjectiveState = ObjectiveState.ManagerOffice;
                    fpsController.enabled = true;
                }
                
            }

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
        if (managersDoor.GetFoundDoor())
        {
            fpsController.enabled = false;

            objectiveDialogue.SetActive(true);

            if(objectiveDialogue.activeInHierarchy == true)
                handlerUI.UpdateObjectiveDialogue("OBJECTIVE UPDATED", objectives[1]);

            if (Timer(ref timer, 4f, true))
            {
                objectiveDialogue.SetActive(false);
                currentObjectiveState = ObjectiveState.FindKey;
                fpsController.enabled = true;
            }

            
        }


        handlerUI.UpdateObjective(objectives[0]);
        
        screenFader.gameObject.SetActive(false);
    }


    private void FindKey()
    {
        if (keycodeFound == true)
        {
            fpsController.enabled = false;

            objectiveDialogue.SetActive(true);

            if (objectiveDialogue.activeInHierarchy == true)
                handlerUI.UpdateObjectiveDialogue("OBJECTIVE UPDATED", objectives[2]);

            if (Timer(ref timer, 4f, true))
            {
                objectiveDialogue.SetActive(false);
                currentObjectiveState = ObjectiveState.KeyFound;
                fpsController.enabled = true;
            }
        }
        
        if(digitFoundDialogue == true)
        {
            DigitFoundDialogue();
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
