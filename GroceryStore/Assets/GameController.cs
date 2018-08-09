using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private bool npcTalking = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetNPCTalking(bool inBool)
    {
        npcTalking = inBool;
    }

    public bool GetNPCTalking()
    {
        return npcTalking;
    }
}
