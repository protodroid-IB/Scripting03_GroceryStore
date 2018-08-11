using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour
{
    private Animator doorAnim;

    private bool closed = true;

    private BoxCollider doorCollider;

	// Use this for initialization
	void Start ()
    {
        doorCollider = transform.GetChild(1).GetComponent<BoxCollider>();
        doorAnim = transform.GetChild(1).GetComponent<Animator>();
	}


    private void Open()
    {
        doorAnim.SetTrigger("Open");
        closed = false;
    }

    private void Close()
    {
        doorAnim.SetTrigger("Close");
        closed = true;
    }

    public void Interact()
    {
        if (closed == true) Open();
        else Close();

    }
}
