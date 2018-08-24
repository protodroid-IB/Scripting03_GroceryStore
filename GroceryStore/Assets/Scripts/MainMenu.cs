using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private ScreenFader screenFader;

    private bool loadGame = false;
    private bool loadCredits = false;

	// Use this for initialization
	void Start ()
    {
        screenFader = GameObject.FindWithTag("ScreenFader").GetComponent<ScreenFader>();

        screenFader.FadeFromBlack(1f);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(loadGame == true)
        {
            if(screenFader.FadeDone())
            {
                SceneManager.LoadScene("Game");
            }
        }

        if (loadCredits == true)
        {
            if (screenFader.FadeDone())
            {
                SceneManager.LoadScene("Credits");
            }
        }
    }



    public void LoadGame()
    {
        loadGame = true;
        screenFader.FadeToBlack(1f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        loadCredits = true;
        screenFader.FadeToBlack(1f);
    }
}
