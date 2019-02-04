using UnityEngine;
using UnityEngine.UI;

public class btnCloseHighscores : MonoBehaviour {
    public Button btn;
	// script used to close the highscores screen
	void Start () {
        btn.onClick.AddListener(Task);
	}

    // resume te game and disable the highscores canvas
    public void Task()
    {
        Time.timeScale = 1;
        FindObjectOfType<btnScoresOnClick>().highscores.enabled = false;
        FindObjectOfType<btnScoresOnClick>().userInterface.enabled = true;
        FindObjectOfType<btnScoresOnClick>().info.enabled = false;
        MoveRandomly.hover = true;
    }
}