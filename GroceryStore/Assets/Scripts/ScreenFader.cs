using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    private Image blackImage;
    private bool fadeDone = false;

    private bool fadeFromBlack = false;
    private bool fadeToBlack = false;

    private Color opaque;
    private Color transparent;

    private float alpha;
    private float fadeSpeed;
    private float fadeAcceleration = 0.01f;

	// Use this for initialization
	void Start ()
    {
        blackImage = transform.GetChild(0).GetComponent<Image>();

        alpha = blackImage.color.a;

        opaque = blackImage.color;
        transparent = new Color(opaque.r, opaque.g, opaque.b, 0f);

	}
	
	// Update is called once per frame
	void Update ()
    {
		if(fadeFromBlack == true)
        {
            Fade(true, fadeSpeed);
            fadeSpeed += fadeAcceleration;
        }
        
        if(fadeToBlack == true)
        {
            Fade(false, fadeSpeed);
            fadeSpeed += fadeAcceleration;
        }
	}





    private void Fade(bool fromBlack, float speed)
    {
        if (fromBlack == true)
        {
            alpha -= speed * Time.deltaTime;

            if (alpha <= 0f) alpha = 0f;

            if (blackImage.color == transparent)
            {
                fadeFromBlack = false;
                fadeDone = true;
            }
        }
        else
        {
            alpha += speed * Time.deltaTime;

            if (alpha >= 1f) alpha = 1f;

            if (blackImage.color == opaque)
            {
                fadeToBlack = false;
                fadeDone = true;
            }
        }

        blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, alpha);
    }




    public void FadeFromBlack(float speed)
    {
        fadeDone = false;
        fadeFromBlack = true;
        fadeSpeed = speed;
    }


    public void FadeToBlack(float speed)
    {
        fadeDone = false;
        fadeToBlack = true;
        fadeSpeed = speed;
    }


    public bool FadeDone()
    {
        return fadeDone;
    }
}
