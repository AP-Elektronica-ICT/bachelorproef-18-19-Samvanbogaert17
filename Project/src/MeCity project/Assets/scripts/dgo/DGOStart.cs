using UnityEngine;
using UnityEngine.UI;

public class DGOStart : MonoBehaviour {
    public Canvas problemCanvas;
    public Canvas eventCanvas;
    public Canvas pauseCanvas;
    public Canvas introCanvas;
    public Canvas endOfGameCanvas;
    public Text txtPlayer;
    
    // script used for disabling the canvasses at the start and to update the player name and score field
	void Start () {
        introCanvas.enabled = true;
        problemCanvas.enabled = false;
        eventCanvas.enabled = false;
        pauseCanvas.enabled = false;
        endOfGameCanvas.enabled = false;
	}
    private void Update()
    {
        txtPlayer.text = "Player: " + DataScript.GetName() + " Score: " + DataScript.GetScore();
    }
}