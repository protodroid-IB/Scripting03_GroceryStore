using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDialogueSound : MonoBehaviour
{
    private Animator dialogueAnimator;
    private AudioSource dialogueSound;

    private bool playedSound;

	// Use this for initialization
	void Start ()
    {
        dialogueAnimator = transform.GetChild(0).GetComponent<Animator>();
        dialogueSound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update()
    {
		if(dialogueAnimator.GetCurrentAnimatorStateInfo(0).IsName("ObjectiveDialogueWindow"))
        {
            if (playedSound == false)
            {
                dialogueSound.Play();
                playedSound = true;
            }
        }
    }



    private void OnDisable()
    {
        playedSound = false;
    }
}
