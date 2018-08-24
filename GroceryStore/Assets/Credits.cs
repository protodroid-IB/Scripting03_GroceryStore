using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Credits : MonoBehaviour
{

    private ScreenFader screenFader;

    private bool fadeToMenu = false;

    private void Start()
    {
        screenFader = GameObject.FindWithTag("ScreenFader").GetComponent<ScreenFader>();

        screenFader.FadeFromBlack(1f);
    }


    public void MainMenu()
    {
        if (fadeToMenu == false)
        {
            screenFader.FadeToBlack(1f);
            fadeToMenu = true;
        }
    }

    private void Update()
    {
        if (fadeToMenu == true && screenFader.FadeDone() == true)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
