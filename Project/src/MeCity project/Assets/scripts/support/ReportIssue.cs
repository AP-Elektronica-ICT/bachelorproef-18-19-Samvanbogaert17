using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReportIssue : MonoBehaviour
{
    public InputField titleField;
    public InputField descriptionField;

    public Text commentText;
    public Button confirmBtn;
    // Start is called before the first frame update
    void Start()
    {
        commentText.text = "";
        XMLManager.instance.LoadReports();
        confirmBtn.onClick.AddListener(ConfirmReport);
    }

    void ConfirmReport()
    {
        //throw error if inputfields are left blank
        if(titleField.text == "" || descriptionField.text == "")
        {
            commentText.color = new Color(1, 0, 0); // make text color red
            commentText.text = "Please fill in a title and a description.";
        }
        //confirm report
        else
        {
            string title = titleField.text;
            string description = descriptionField.text;

            XMLManager.instance.AddReport(title, description);
            XMLManager.instance.SaveReports(false);

            EmailController.instance.SendEmail("Report", title, description, DateTime.Now.ToString(new CultureInfo("en-GB")));

            titleField.text = "";
            descriptionField.text = "";
            commentText.color = new Color(0, 1, 0); // make text color green
            commentText.text = "Thank you for reporting an issue!";
        }
    }
}
