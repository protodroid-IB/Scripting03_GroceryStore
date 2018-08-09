using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class NPCDialogue : MonoBehaviour
{
    private bool isTalking = false;
    private FirstPersonController fpsController;
    private GameController gameController;

    // Use this for initialization
    void Start ()
    {
        fpsController = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Talk()
    {
        isTalking = true;
        Debug.Log("NPC TALK!");

        fpsController.enabled = false;
        gameController.SetNPCTalking(isTalking);

        Invoke("NotTalking", 3f);
    }

    public void SetTalking(bool inTalking)
    {
        isTalking = inTalking;
    }

    public bool IsTalking()
    {
        return isTalking;
    }

    private void NotTalking()
    {
        isTalking = false;
        fpsController.enabled = true;
        gameController.SetNPCTalking(isTalking);
    }
}
