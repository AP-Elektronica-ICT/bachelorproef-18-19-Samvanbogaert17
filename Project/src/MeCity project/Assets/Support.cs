using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas reportCanvas;
    public Canvas suggestionCanvas;
    public Canvas addQuestionCanvas;

    private void Start()
    {
        menuCanvas.enabled = true;
        reportCanvas.enabled = false;
        suggestionCanvas.enabled = false;
        addQuestionCanvas.enabled = false;
    }
}
