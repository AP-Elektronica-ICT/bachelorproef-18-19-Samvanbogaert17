using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnScoresOnClick : MonoBehaviour {

    public Button btn;
    public Canvas highscores;
    public Canvas userInterface;
    public Canvas info;
    public Text number;
    public Text name;
    public Text score;

    // script used to open the top highscores canvas
    public void Start()
    {
        btn.onClick.AddListener(Task);
    }
    public void Task()
    {
        // pause the game and disable all canvases except for the highscore canvas
        Time.timeScale = 0;
        MoveRandomly.hover = false;
        info.enabled = false;
        userInterface.enabled = false;
        highscores.enabled = true;
        string temp1 = "";
        string temp2 = "";
        string temp3 = "";
        List<int> numbers = new List<int>();

        // add every player and score to the textfields
        for (int i = 0; i < FindObjectOfType<addFish>().lines.Length; i++)
        { 
            string naam = FindObjectOfType<addFish>().lines[i];
            string[] eerste = naam.Split(':');
            FindObjectOfType<addFish>().txtPlayer.text = eerste[0];
            temp1 +="Player: " + eerste[0] + "\n";

            string test = FindObjectOfType<addFish>().lines[i];
            string[] laatste = test.Split(':');
            FindObjectOfType<addFish>().txtScore.text = laatste[1];
            temp2 += "Score: " + float.Parse(laatste[1]) + "\n";

            numbers.Add(i + 1);
            temp3 += numbers[i].ToString() + ".\n";
        }
        name.text = temp1;
        score.text = temp2;
        number.text = temp3;
    }
}