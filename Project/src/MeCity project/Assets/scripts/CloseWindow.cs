using UnityEngine;

public class CloseWindow : MonoBehaviour {

    // script used to close a window
    public void Close()
    {
        CameraControl.showingPopUp = false;
        GetComponent<Canvas>().enabled = false;
    }

    public void ClosePanel()
    {
        CameraControl.showingPopUp = false;
        GetComponent<GameObject>().SetActive(false);
    }
}