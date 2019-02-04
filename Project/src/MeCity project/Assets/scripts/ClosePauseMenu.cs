using UnityEngine;
using UnityEngine.UI;

public class ClosePauseMenu : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Button btn;
    public Canvas canvas;

    // script used to close the pause canvas
    public void Start()
    {
        btn.onClick.AddListener(Task);
    }
    public void Task()
    {
        CameraControl.showingPopUp = false;
        Time.timeScale = 1;
        canvas.enabled = true;
        pauseCanvas.enabled = false;
    }
}