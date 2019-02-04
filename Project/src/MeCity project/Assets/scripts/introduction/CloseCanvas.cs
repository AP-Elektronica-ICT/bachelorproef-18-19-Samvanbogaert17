using UnityEngine;

public class CloseCanvas : MonoBehaviour {
    // script used to close the producer canvas and play the city overview animation
    public void Close()
    {
        Camera.main.GetComponent<Animation>().Play("OverviewAnimation");
        GetComponent<Canvas>().enabled = false;
    }
}