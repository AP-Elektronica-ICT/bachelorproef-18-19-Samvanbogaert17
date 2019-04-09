using UnityEngine;

public class StartIntroduction : MonoBehaviour {

    public Canvas infoCanvas;
    public Canvas meganCanvas;
    public Canvas startCanvas;
    public Canvas supplierCanvas;
    public Canvas dgoCanvas;
    public Canvas tgoCanvas;
    public Canvas producer;
    public Canvas pauseCanvas;

    // script used to start the introduction level
    // disable all the canvasses except for the ui canvas (logo and pause button)
    void Start () {
        infoCanvas.enabled = true;
        meganCanvas.enabled = false;
        supplierCanvas.enabled = false;
        startCanvas.enabled = false;
        dgoCanvas.enabled = false;
        tgoCanvas.enabled = false;
        producer.enabled = false;
        pauseCanvas.enabled = false;
    }
}