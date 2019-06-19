using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public Button btn;
    public InputField field;
    string naam;
    public Text validation;

    // script used to validate the user username input field and writes the player name and beginning score to the datascript
    void Start()
    {
        btn.onClick.AddListener(Task);
        Update();
    }
    // also works when pressing enter
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Task();
        }
    }

    public void Task()
    {
        // input field validation
        naam = field.text;
        if (naam.Equals(""))
        {
            validation.text = "Invalid username: please give me a username";
        }
        else if (naam.Length >= 20)
        {
            validation.text = "Invalid username: username too long";
        }
        else
        {
            if (naam.Contains(":"))
            {
                validation.text = ": not allowed in username";
            }
            else
            {
                // save the player name and score
                DataScript.SetName(naam);
                DataScript.SetScore(10);
                SceneManager.LoadScene("titlescreen");
            }
        }
    }
}