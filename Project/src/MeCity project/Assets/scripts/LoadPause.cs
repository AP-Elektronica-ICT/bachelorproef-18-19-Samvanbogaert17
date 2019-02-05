using UnityEngine;
using UnityEngine.UI;

public class LoadPause : MonoBehaviour {

    public Canvas pauseCanvas;
    public Button btn;
    public Canvas canvas;
    public Canvas intro;
    public static bool paused;

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
            Time.timeScale = 0;
            paused = true;
            pauseCanvas.enabled = true;
            canvas.enabled = false;
        }
    }
}