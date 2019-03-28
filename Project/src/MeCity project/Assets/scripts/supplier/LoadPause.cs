using UnityEngine;
using UnityEngine.UI;

public class LoadPause : MonoBehaviour {

    public Canvas pauseCanvas;
    public Button btn;
    public Canvas uiCanvas;

    // script used for pausing the game
    private void Start()
    {
        btn.onClick.AddListener(Task);
    }
    public void Task()
    {
        if (!CameraControl.showingPopUp)
        {
            CameraControl.showingPopUp = true;
            CameraControl.paused = true;
            Time.timeScale = 0;
            pauseCanvas.enabled = true;
            uiCanvas.enabled = false;
        }
    }
}