using UnityEngine;

public class CloseWindow : MonoBehaviour {

    // script used to close a window
    public void Close()
    {
        CameraControl.showingPopUp = false;
        this.GetComponent<Canvas>().enabled = false;
    }
}