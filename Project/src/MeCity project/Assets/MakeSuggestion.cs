using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeSuggestion : MonoBehaviour
{
    public InputField titleField;
    public InputField descriptionField;

    public Text commentText;
    public Button confirmBtn;
    // Start is called before the first frame update
    void Start()
    {
        commentText.text = "";
        Debug.Log(XMLManager.instance);
        XMLManager.instance.LoadReports();
        confirmBtn.onClick.AddListener(ConfirmReport);
    }

    void ConfirmReport()
    {
        //throw error if inputfields are left blank
        if (titleField.text == "" || descriptionField.text == "")
        {
            commentText.color = new Color(1, 0, 0); // make text color red
            commentText.text = "Please fill in a title and a description.";
        }
        //confirm report
        else
        {
            string title = titleField.text;
            string description = descriptionField.text;
            XMLManager.instance.AddSuggestion(title, description);
            XMLManager.instance.SaveSuggestions();
            titleField.text = "";
            descriptionField.text = "";
            commentText.color = new Color(0, 1, 0); // make text color red
            commentText.text = "Thank you for making a suggestion!";
        }
    }
}
