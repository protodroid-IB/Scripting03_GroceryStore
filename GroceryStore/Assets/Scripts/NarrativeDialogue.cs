using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script Name: NarrativeDialogue.cs
// Written By: Laurence Valentini


public class NarrativeDialogue : MonoBehaviour
{
    private ScreenFader screenFader;

    [System.Serializable]
    public struct Dialogue
    {
        public string speakersName;
        [Range(1f, 50f)]
        public float textSpeed;
        public Color textColor;
        [TextArea]
        public string dialogue;
    }

    [SerializeField]
    Dialogue[] introNarrative, outroNarrative;

    private int narrativeIndex = 0;

    private bool introNarrativeStart = false, introNarrativeDone = false;
    private bool outroNarrativeStart = false, outroNarrativeDone = false;

    private Text dialogue;

    private string outDialogue = "";
    private int typingIndex = 0;
    private float typingTime;
    private bool typingDone = false;
    private float typingSpeed = 22f;
    private AudioSource typingSound;
    private int lastTypingIndex = -1;

    private float alpha;

	// Use this for initialization
	void Start ()
    {
        screenFader = GameObject.FindWithTag("ScreenFader").GetComponent<ScreenFader>();
        dialogue = GameObject.FindWithTag("ScreenFader").transform.GetChild(1).GetComponent<Text>();
        typingSound = GameObject.FindWithTag("ScreenFader").transform.GetChild(1).GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(introNarrativeStart == true)
        {
            if (introNarrativeDone == false)
            {
                IntroNarrative();
            }
            
        }

        if (outroNarrativeStart == true)
        {
            if (outroNarrativeDone == false)
            {
                OutroNarrative();
            }
        }
	}



    private void IntroNarrative()
    {
        if(narrativeIndex < introNarrative.Length)
        {
            if (typingDone == false)
            {
                typingSpeed = introNarrative[narrativeIndex].textSpeed;
                dialogue.color = introNarrative[narrativeIndex].textColor;
                dialogue.text = TypeDialogue(introNarrative[narrativeIndex].dialogue);
            }

            alpha = dialogue.color.a;
        }
        else
        {

            alpha -= 1f * Time.deltaTime;

            if (alpha <= 0f) alpha = 0f;

            Color newColor = new Color(dialogue.color.r, dialogue.color.g, dialogue.color.b, alpha);

            dialogue.color = newColor;

            if(alpha <= 0f)
            {
                introNarrativeDone = true;
                introNarrativeStart = false;
                Invoke("FadeIntoGame", 1f);
                narrativeIndex = 0;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(typingDone == true)
            {
                narrativeIndex++;
                typingDone = false;
                outDialogue = "";
            }
            
        }
    }




    private void OutroNarrative()
    {
        if(screenFader.FadeDone())
        {
            if (narrativeIndex < outroNarrative.Length)
            {
                if (typingDone == false)
                {
                    typingSpeed = outroNarrative[narrativeIndex].textSpeed;
                    dialogue.color = outroNarrative[narrativeIndex].textColor;
                    dialogue.text = TypeDialogue(outroNarrative[narrativeIndex].dialogue);
                }

                alpha = dialogue.color.a;
            }
            else
            {

                alpha -= 1f * Time.deltaTime;

                if (alpha <= 0f) alpha = 0f;

                Color newColor = new Color(dialogue.color.r, dialogue.color.g, dialogue.color.b, alpha);

                dialogue.color = newColor;

                if (alpha <= 0f)
                {
                    outroNarrativeDone = true;
                    outroNarrativeStart = false;
                    narrativeIndex = 0;

                    // run credits

                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (typingDone == true)
                {
                    narrativeIndex++;
                    typingDone = false;
                    outDialogue = "";
                }

            }
        }
        
        
    }





    public void StartIntroNarrative()
    {
        introNarrativeStart = true;
    }

    public void StartOutroNarrative()
    {
        outroNarrativeStart = true;
        FadeOutOfGame();
    }

    public bool IntroNarrativeDone()
    {
        return introNarrativeDone;
    }

    public bool OutroNarrativeDone()
    {
        return outroNarrativeDone;
    }

    private void FadeIntoGame()
    {
        screenFader.FadeFromBlack(0.01f);
    }

    private void FadeOutOfGame()
    {
        screenFader.FadeToBlack(0.01f);
    }







    private string TypeDialogue(string inDialogue)
    {
        

        if (typingIndex <= inDialogue.Length)
        {
            if (lastTypingIndex != typingIndex)
            {
                typingSound.Play();
                lastTypingIndex = typingIndex;
            }

            outDialogue = inDialogue.Substring(0, typingIndex);

            typingTime += Time.deltaTime * typingSpeed;
            typingIndex = (int)typingTime;
        }
        else
        {
            typingTime = 0f;
            typingIndex = 0;
            typingDone = true;
        }

        return outDialogue;
    }


    public bool GetOutroNarrativeStart()
    {
        return outroNarrativeStart;
    }
}
