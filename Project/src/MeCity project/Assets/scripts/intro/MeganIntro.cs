using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MeganIntro : MonoBehaviour {

    public Button btn;
    public Text txt;
    // script used for the Megan intro
    void Start()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(Task);
    }
    public void Task()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(LoadNext);
        txt.text = "This game is developed by students in the context of the on boarding program for new employees. Goal of the game is to understand how the energy market works, " +
            "which roles there are and who is responsible for what."+
            "\n\nThere ought to be some bugs left, " +
            "so we count on your constructive and positive attitude to play the game and give contructive feedback through the support page in the game, or mention it directly to the gamification team. " +
            "Also, don't forget to have fun and enjoy your playthrough!";
    }
    // load the introduction level scene
    public void LoadNext()
    {
        SceneManager.LoadScene("Introduction");
    }
}