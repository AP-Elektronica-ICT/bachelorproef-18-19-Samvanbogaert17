using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TGOSimonSaysButton : MonoBehaviour
{
    private Image img;
    private AudioSource sound;
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        sound = GetComponent<AudioSource>();
    }

    //function that is called when the mouse starts hovering over the image
    //Only returns true once
    public void OnPointerDown()
    {
        //changes the alpha value of the current color to 1f or no transparency
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);

        //starts playing the sound attached to the button
        sound.Play();
    }

    //function that is called when the mouse leaves the image
    //Only returns true once
    public void OnPointerUp()
    {
        //changes the alpha value of the current color to 0.5f or half transparency
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f);

        //stops playing the sound attached to the button
        sound.Stop();
    }
}
