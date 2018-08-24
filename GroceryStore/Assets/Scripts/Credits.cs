using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script Name: Credits.cs
// Written By: Laurence Valentini

public class Credits : MonoBehaviour
{
    private ScreenFader screenFader; // grab the screen fader

    private bool fadeToMenu = false; // tracks when the button has been clicked to fade to menu


    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // grab screen fader
        screenFader = GameObject.FindWithTag("ScreenFader").GetComponent<ScreenFader>();

        // start fading from black
        screenFader.FadeFromBlack(1f);
    }



    // this is the method that is run when the main menu button is pressed
    public void MainMenu()
    {
        // if the screen fader hasn't started fading yet
        if (fadeToMenu == false)
        {
            // start fading to black and set the reserved boolean
            screenFader.FadeToBlack(1f);
            fadeToMenu = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            MainMenu();
        }

        // if the main menu button has been pressed and the screen fader has completely faded to black
        if (fadeToMenu == true && screenFader.FadeDone() == true)
        {
            // load the main menu scene
            SceneManager.LoadScene("MainMenu");
        }
    }
}
