using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Linq;
using System.Globalization;
using System.Text;

public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;

    //filepaths located in the global transfer drive in the Ferranti environment;
    private readonly string globalHighscoreXML = @"I:\svboga\MeCity\Highscores/highscores.xml";
    private readonly string globalReportXML = @"I:\svboga\MeCity\Reports/reports.xml";
    private readonly string globalSuggestionXML = @"I:\svboga\MeCity\Suggestions/suggestions.xml";
    private readonly string globalUnconfirmedQuestionXML = @"I:\svboga\MeCity\Questions\Unconfirmed/questions.xml";
    private readonly string globalConfirmedQuestionXML = @"I:\svboga\MeCity\Questions\Confirmed/questions.xml";


    private string localHighscoreXML;
    private string localReportXML;
    private string localSuggestionXML;
    private string localQuestionXML;


    private void Awake()
    {
        instance = this;

        localHighscoreXML = Application.persistentDataPath + "/Highscores/highscores.xml";
        localReportXML = Application.persistentDataPath + "/Reports/reports.xml";
        localSuggestionXML = Application.persistentDataPath + "/Suggestions/suggestions.xml";
        localQuestionXML = Application.persistentDataPath + "/Questions/questions.xml";
    }

    //
    //All code regarding saving and loading highscores
    //

    [HideInInspector] public HighscoreDatabase highscoreDB = new HighscoreDatabase();

    //save
    public void SaveHighscores()
    {
        //open a new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(HighscoreDatabase));
        FileStream stream = new FileStream(globalHighscoreXML, FileMode.Create, FileAccess.ReadWrite);
        serializer.Serialize(stream, highscoreDB);
        stream.Close();

        //Encryption.EncryptFile(globalHighscoreXML);
    }

    //load
    public void LoadHighscores()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(HighscoreDatabase));
        if (File.Exists(globalHighscoreXML))
        {
            //Encryption.DecryptFile(globalHighscoreXML);

            FileStream stream = new FileStream(globalHighscoreXML, FileMode.Open, FileAccess.ReadWrite);
            highscoreDB = serializer.Deserialize(stream) as HighscoreDatabase;
            stream.Close();
        }
        else
        {
            SaveHighscores();
        }
    }

    // Reorder db highest to lowest score
    public void ReorderHighscores()
    {
        highscoreDB.list = highscoreDB.list.OrderByDescending(item => int.Parse(item.highscore)).ToList();
    }

    public void AddHighscore(string username, string highscore)
    {
        if (!username.IsOneOf("", ":"))
        {
            HighscoreEntry entry = new HighscoreEntry
            {
                username = username,
                highscore = highscore
            };
            highscoreDB.list.Add(entry);
        }
    }

    public void ModifyHighscore(string username, string highscore)
    {
        if (!username.IsOneOf("", ":"))
        {
            int index = highscoreDB.list.FindIndex(item => item.username == username);
            if (int.Parse(highscore) > int.Parse(highscoreDB.list[index].highscore))
            {
                highscoreDB.list[index].highscore = highscore;
            }
        }
    }

    //
    //All code regarding saving and loading reports
    //

    [HideInInspector] public ReportDatabase reportDB = new ReportDatabase();

    public void SaveReports()
    {
        //open a new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(ReportDatabase));
        FileStream stream = new FileStream(globalReportXML, FileMode.Create, FileAccess.ReadWrite);
        serializer.Serialize(stream, reportDB);
        stream.Close();

        //Encryption.EncryptFile(globalReportXML);
    }

    public void LoadReports()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ReportDatabase));
        if (File.Exists(globalReportXML))
        {
            //Encryption.DecryptFile(globalReportXML);

            FileStream stream = new FileStream(globalReportXML, FileMode.Open, FileAccess.ReadWrite);
            reportDB = serializer.Deserialize(stream) as ReportDatabase;
            stream.Close();
        }
        else
        {
            SaveReports();
        }
    }

    public void AddReport(string title, string description)
    {
        ReportEntry entry = new ReportEntry
        {
            title = title,
            description = description,
            dateLogged = string.Format("logged on: {0}", DateTime.Now.ToString(new CultureInfo("en-GB")))
        };
        reportDB.list.Add(entry);
    }

    public void RemoveReport(int index)
    {
        reportDB.list.RemoveAt(index);
    }

    //
    //All code regarding saving and loading suggestions
    //

    [HideInInspector] public SuggestionDatabase suggestionDB = new SuggestionDatabase();

    public void SaveSuggestions()
    {
        //open a new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(SuggestionDatabase));
        FileStream stream = new FileStream(globalSuggestionXML, FileMode.Create, FileAccess.ReadWrite);
        serializer.Serialize(stream, suggestionDB);
        stream.Close();

        //Encryption.EncryptFile(globalSuggestionXML);
    }

    public void LoadSuggestions()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SuggestionDatabase));
        if (File.Exists(globalSuggestionXML))
        {
            //Encryption.DecryptFile(globalSuggestionXML);

            FileStream stream = new FileStream(globalSuggestionXML, FileMode.Open, FileAccess.ReadWrite);
            suggestionDB = serializer.Deserialize(stream) as SuggestionDatabase;
            stream.Close();
        }
        else
        {
            SaveSuggestions();
        }
    }

    public void AddSuggestion(string title, string description)
    {
        SuggestionEntry entry = new SuggestionEntry
        {
            title = title,
            description = description,
            dateLogged = string.Format("logged on: {0}", DateTime.Now.ToString(new CultureInfo("en-GB")))
        };
        suggestionDB.list.Add(entry);
    }

    public void RemoveSuggestion(int index)
    {
        suggestionDB.list.RemoveAt(index);
    }

    //
    //All code regarding saving and loading questions
    //
    [HideInInspector] public QuestionDatabase questionDB = new QuestionDatabase();

    public void SaveQuestions(bool saveToGlobal = true)
    {
        if (saveToGlobal)
        {
            //open a new xml file
            XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
            FileStream stream = new FileStream(globalUnconfirmedQuestionXML, FileMode.Create, FileAccess.ReadWrite);
            using (StreamWriter _stream = new StreamWriter(stream, Encoding.GetEncoding("UTF-8")))
            {
                serializer.Serialize(stream, questionDB);
            }
            stream.Close();
        }
        else
        {
            //open a new xml file
            XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
            FileStream stream = new FileStream(localQuestionXML, FileMode.Create, FileAccess.ReadWrite);
            using (StreamWriter _stream = new StreamWriter(stream, Encoding.GetEncoding("UTF-8")))
            {
                serializer.Serialize(stream, questionDB);
            }
            stream.Close();
        }
        //Encryption.EncryptFile(globalUnconfirmedQuestionXML);
    }

    public void LoadQuestions()
    {
        if (File.Exists(localQuestionXML))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
            FileStream stream = new FileStream(localQuestionXML, FileMode.Open, FileAccess.ReadWrite);
            using (StreamReader _stream = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
            {
                questionDB = serializer.Deserialize(stream) as QuestionDatabase;
            }
            stream.Close();
        }
        else if (File.Exists(globalUnconfirmedQuestionXML))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(QuestionDatabase));
            FileStream stream = new FileStream(globalUnconfirmedQuestionXML, FileMode.Open, FileAccess.ReadWrite);
            using (StreamReader _stream = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
            {
                questionDB = serializer.Deserialize(stream) as QuestionDatabase;
            }
            stream.Close();

            SaveQuestions(false);
        }
        else
        {
            SaveQuestions();
        }
    }

    public void AddQuestion(string _subject, string _question, List<Answer> _answers)
    {
        if (questionDB.list.Any(item => item.subject == _subject))
        {
            foreach (QuestionList _questionList in questionDB.list)
            {
                if (_questionList.subject == _subject)
                {
                    QuestionEntry entry = new QuestionEntry
                    {
                        question = _question,
                        answers = _answers
                    };
                    _questionList.questionEntries.Add(entry);
                }
            }
        }
        else
        {
            List<QuestionEntry> entryList = new List<QuestionEntry>();
            entryList.Add(new QuestionEntry
            {
                question = _question,
                answers = _answers
            });
            questionDB.list.Add(new QuestionList { subject = _subject, questionEntries = entryList });
        }
    }

    public void AddDilemma(string _subject, string _question, List<Dilemma> _dilemmas)
    {
        if (questionDB.list.Any(item => item.subject == _subject))
        {
            foreach (QuestionList _questionList in questionDB.list)
            {
                if (_questionList.subject == _subject)
                {
                    DilemmaEntry entry = new DilemmaEntry
                    {
                        question = _question,
                        dilemmas = _dilemmas

                    };
                    _questionList.dilemmaEntries.Add(entry);
                }
            }
        }
        else
        {
            List<DilemmaEntry> entryList = new List<DilemmaEntry>();
            entryList.Add(new DilemmaEntry
            {
                question = _question,
                dilemmas = _dilemmas
            });
            questionDB.list.Add(new QuestionList { subject = _subject, dilemmaEntries = entryList });
        }
    }

    public void AddCorrectOrder(string _subject, string _question, List<Position> _positions)
    {
        if (questionDB.list.Any(item => item.subject == _subject))
        {
            foreach (QuestionList _questionList in questionDB.list)
            {
                if (_questionList.subject == _subject)
                {
                    CorrectOrderEntry entry = new CorrectOrderEntry
                    {
                        question = _question,
                        positions = _positions

                    };
                    _questionList.correctOrderEntries.Add(entry);
                }
            }
        }
        else
        {
            List<CorrectOrderEntry> entryList = new List<CorrectOrderEntry>();
            entryList.Add(new CorrectOrderEntry
            {
                question = _question,
                positions = _positions
            });
            questionDB.list.Add(new QuestionList { subject = _subject, correctOrderEntries = entryList });
        }
    }

    public void RemoveQuestion()
    {

    }

    public void ModifyQuestion()
    {

    }

}

//serializable highscore classes
[Serializable]
public class HighscoreEntry
{
    public string username;
    public string highscore;
}

[Serializable]
public class HighscoreDatabase
{
    public List<HighscoreEntry> list = new List<HighscoreEntry>();
}

//serializable report classes
[Serializable]
public class ReportEntry
{
    public string title;
    public string description;
    public string dateLogged;
}

[Serializable]
public class ReportDatabase
{
    public List<ReportEntry> list = new List<ReportEntry>();
}

//serializable suggestion classes
[Serializable]
public class SuggestionEntry
{
    public string title;
    public string description;
    public string dateLogged;
}

[Serializable]
public class SuggestionDatabase
{
    public List<SuggestionEntry> list = new List<SuggestionEntry>();
}

//serializable question classes
[Serializable]
public class QuestionEntry
{
    public string question;

    public List<Answer> answers = new List<Answer>();
}

[Serializable]
public class Answer
{
    [XmlAttribute("modifier")]
    public int modifier;

    public string answer;
}

[Serializable]
public class DilemmaEntry
{
    public string question;

    public List<Dilemma> dilemmas = new List<Dilemma>();
}

[Serializable]
public class Dilemma
{
    [XmlAttribute("consumption")]
    public int consumption = 0;

    [XmlAttribute("money")]
    public int money = 0;

    [XmlAttribute("energy")]
    public int energy = 0;

    public string choice;
}

[Serializable]
public class CorrectOrderEntry
{
    public string question;

    public List<Position> positions = new List<Position>();
}

[Serializable]
public class Position
{
    public string position;
}

[Serializable]
public class QuestionList
{
    [XmlAttribute("subject")]
    public string subject;

    public List<QuestionEntry> questionEntries = new List<QuestionEntry>();
    public List<DilemmaEntry> dilemmaEntries = new List<DilemmaEntry>();
    public List<CorrectOrderEntry> correctOrderEntries = new List<CorrectOrderEntry>();
}

[Serializable]
public class QuestionDatabase
{
    public List<QuestionList> list = new List<QuestionList>();
}