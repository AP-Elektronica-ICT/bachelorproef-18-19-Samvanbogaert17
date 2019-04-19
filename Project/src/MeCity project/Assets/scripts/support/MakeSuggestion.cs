using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
            //read inputfields
            string title = titleField.text;
            string description = descriptionField.text;
            //adds suggestion title & description to the suggestion database
            XMLManager.instance.AddSuggestion(title, description);
            //save the suggestion database
            XMLManager.instance.SaveSuggestions();
            //send an email to dev.ferranti@gmail.com about the newly logged suggestion
            EmailController.instance.SendEmail("Suggestion", title, description, DateTime.Now.ToString(new CultureInfo("en-GB")));

            titleField.text = "";
            descriptionField.text = "";
            commentText.color = new Color(0, 1, 0); // make text color green
            commentText.text = "Thank you for making a suggestion!";
        }
    }
}
