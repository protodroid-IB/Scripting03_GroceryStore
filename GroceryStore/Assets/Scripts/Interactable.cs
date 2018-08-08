using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public bool Hover()
    {
        Debug.Log("HOVERING");

        // display UI data

        // item glow

        return true;
    }

    public void Interact()
    {
        Debug.Log("INTERACTING");

        // add item to inventory
    }
}
