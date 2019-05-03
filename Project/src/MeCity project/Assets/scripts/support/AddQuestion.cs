using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddQuestion : MonoBehaviour
{
    public GameObject pickPanel;
    public GameObject questionPanel;

    public Text commentTxt;
    public Button backBtn;

    public Button confirmBtn;
    public Button[] pickBtns;

    public InputField questionInput;
    public InputField correctAnsInput;
    public InputField[] falseAnsInputs;

    private string subject;
    // Start is called before the first frame update
    void Start()
    {
        //closes question panel that will be enabled by default
        CloseQuestionPanel();
        // Loads all the questions in the unconfirmed questions file path
        XMLManager.instance.LoadQuestions();
        //
        backBtn.onClick.AddListener(CloseQuestionPanel);
        //Add onclick listeners to the pick buttons
        for(int i = 0; i < pickBtns.Length; i++)
        {
            string _subject = pickBtns[i].GetComponentInChildren<Text>().text;
            pickBtns[i].onClick.AddListener(() => {
                OpenQuestionPanel(_subject);
            });
        }

        confirmBtn.onClick.AddListener(Confirm);
    }

    //method for picking what the question is about
    void OpenQuestionPanel(string _subject)
    {
        subject = "";
        subject = _subject;
        pickPanel.SetActive(false);
        questionPanel.SetActive(true);
    }

    void CloseQuestionPanel()
    {
        commentTxt.text = "";
        pickPanel.SetActive(true);
        questionPanel.SetActive(false);
    }

    void Confirm()
    {
        // throws error if question texts and atleast the first two answer texts are filled in
        if(questionInput.text == "" || (correctAnsInput.text == "" && falseAnsInputs[0].text == ""))
        {
            commentTxt.color = new Color(1, 0, 0);
            commentTxt.text = "Please fill in a question with atleast 1 correct and 1 false answer.";
        }// confirms question
        else
        {
            commentTxt.color = new Color(0, 1, 0);
            commentTxt.text = "Submit succesfull!";
            string question = questionInput.text;
            List<Answer> answers = new List<Answer>();
            //Add the correct answer
            answers.Add(new Answer { modifier = 1, answer = correctAnsInput.text });
            //Add all the false answers
            for(int i = 0; i < falseAnsInputs.Length; i++)
            {
                if(falseAnsInputs[i].text != "")
                {
                    answers.Add(new Answer { modifier = -1, answer = falseAnsInputs[i].text });
                }
            }
            //
            EmailController.instance.SendEmail(
                "Question",
                "New Question about " + subject, 
                questionInput.text + "\n\n" + correctAnsInput.text + "\n" + falseAnsInputs[0].text + "\n" + falseAnsInputs[1].text + "\n" + falseAnsInputs[2].text + "\n" + falseAnsInputs[3].text,
                DateTime.Now.ToString(new CultureInfo("en-GB")));

            XMLManager.instance.AddQuestion(subject, question, answers);
            XMLManager.instance.SaveQuestions(false);

            //clear inputs
            questionInput.text = "";
            correctAnsInput.text = "";
            foreach(InputField input in falseAnsInputs)
            {
                input.text = "";
            }
        }
    }
}
