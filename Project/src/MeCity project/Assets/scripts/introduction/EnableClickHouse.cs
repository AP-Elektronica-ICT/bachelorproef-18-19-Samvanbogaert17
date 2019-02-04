using UnityEngine;

public class EnableClickHouse : MonoBehaviour {
    // script used to enable the infoCanvas "To begin, click the house in front of you" 
    public Canvas infoCanvas;
    void Update () {
            infoCanvas.enabled = true;
    }
}