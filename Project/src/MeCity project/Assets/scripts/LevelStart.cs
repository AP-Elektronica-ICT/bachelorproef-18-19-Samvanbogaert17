using UnityEngine;
using UnityEngine.UI;

public class LevelStart : MonoBehaviour
{
    public Canvas introCanvas;
    public Text txtPlayer;
    public Canvas[] canvasArray;

    // script used for disabling the canvasses at the start and to update the player name and score field
    void Start()
    {
        introCanvas.enabled = true;
        for (int i = 0; i < canvasArray.Length; i++)
        {
            canvasArray[i].enabled = false;
        }
    }
    private void Update()
    {
        txtPlayer.text = "Player: " + DataScript.GetName() + " Score: " + DataScript.GetScore();
    }
}
