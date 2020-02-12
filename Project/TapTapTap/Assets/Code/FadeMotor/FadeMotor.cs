

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMotor : MonoBehaviour
{

    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.8f;
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;


    void OnGUI()
    {



        alpha += fadeDir * fadeSpeed * Time.deltaTime; //fading with speed;
        alpha = Mathf.Clamp01(alpha); // clamp this color from a 0 to 1.

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha); //set the color for the fade in a rgb.
        GUI.depth = drawDepth;// draw the black texture.
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture); // draw the texture to the screen.



    }

    public float BeginFade(int direction)
    { // fade direction.

        fadeDir = direction;
        return (fadeSpeed);


    }

    void Awake()
    {

        BeginFade(-1);

    }
}

