using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas reportCanvas;
    public Canvas suggestionCanvas;
    public Canvas addQuestionCanvas;
    public Canvas adminCanvas;

    private void Start()
    {
        menuCanvas.gameObject.SetActive(true);
        reportCanvas.gameObject.SetActive(false);
        suggestionCanvas.gameObject.SetActive(false);
        addQuestionCanvas.gameObject.SetActive(false);
        adminCanvas.gameObject.SetActive(false);
    }
}
