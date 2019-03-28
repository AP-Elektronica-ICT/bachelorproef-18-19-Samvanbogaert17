using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOSimonSays : MonoBehaviour
{
    public Image[] colors;
    public Button[] btns;

    private System.Random rnd = new System.Random();
    private int colorSelect;

    public float stayLit;
    private float stayLitCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stayLit > 0)
        {
            stayLitCounter -= Time.deltaTime;
        }
        else
        {
            colors[colorSelect].color = new Color(colors[colorSelect].color.r, colors[colorSelect].color.g, colors[colorSelect].color.b, 0.5f);
        }
    }

    public void StartGame()
    {
        colorSelect = rnd.Next(0, colors.Length);

        colors[colorSelect].color = new Color(colors[colorSelect].color.r, colors[colorSelect].color.g, colors[colorSelect].color.b, 1f);
    }

    public void ColourPressed(int btnIndex)
    {
        if (colorSelect == btnIndex)
        {

        }
    }
}
