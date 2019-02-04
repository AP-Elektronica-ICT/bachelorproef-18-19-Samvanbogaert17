using UnityEngine;
using UnityEngine.UI;

public class LevelHighscore : MonoBehaviour {
    
    public Text score;

    // loads the player name and score and shows it on the bottom left corner
	void Start () {  
        score.text = "Player: " + DataScript.GetName() + " Score: " +  DataScript.GetScore();
    }
}