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
        txt.text = "This game is developed by 3 students in less then a month time. Goal of the game is to understand how the energy market works," +
            " which roles there are and who is responsible for what. We want to add this to the onboarding program for new employees and it will be a " +
            "fun thing to use on the BOOST 18 event as well. The game won't be complete yet, we are focusing on the introduction level and supplier level " +
            "for this months assignment. \n\n There even might be some bugs left. Our students (Laura, Ryan and Jordan) did not have money resources to make this" +
            " game and therefore had to use free assets. (you can see this by how fancy the main screen looks in comparison with the ingame graphics. So we" +
            " count on your constructive and positive attitude to play the game and give contructive feedback to our gamification team.";
    }
    // load the introduction level scene
    public void LoadNext()
    {
        SceneManager.LoadScene("Introduction");
    }
}