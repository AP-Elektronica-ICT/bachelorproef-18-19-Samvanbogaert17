using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Linq;
using System.Globalization;

public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;

    private string localHighscoreFilePath; //saves file to a local folder in the game files
    //filepaths located in the global transfer drive in the Ferranti environment;
    private readonly string globalHighscoreFilePath = @"I:\svboga\MeCity\Highscores/highscores.xml";
    private readonly string globalReportFilePath = @"I:\svboga\MeCity\Reports/reports.xml";
    private readonly string globalSuggestionFilePath = @"I:\svboga\MeCity\Suggestions/suggestions.xml";

    private void Awake()
    {
        localHighscoreFilePath = Application.dataPath + "/highscores.xml";
        instance = this;
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
        FileStream stream = new FileStream(globalHighscoreFilePath, FileMode.Create, FileAccess.ReadWrite);
        serializer.Serialize(stream, highscoreDB);
        stream.Close();
    }

    //load
    public void LoadHighscores()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(HighscoreDatabase));
        if (File.Exists(globalHighscoreFilePath))
        {
            FileStream stream = new FileStream(globalHighscoreFilePath, FileMode.Open, FileAccess.ReadWrite);
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
        FileStream stream = new FileStream(globalReportFilePath, FileMode.Create, FileAccess.ReadWrite);
        serializer.Serialize(stream, reportDB);
        stream.Close();
    }

    public void LoadReports()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ReportDatabase));
        if (File.Exists(globalReportFilePath))
        {
            FileStream stream = new FileStream(globalReportFilePath, FileMode.Open, FileAccess.ReadWrite);
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

    //
    //All code regarding saving and loading suggestions
    //

    [HideInInspector] public SuggestionDatabase suggestionDB = new SuggestionDatabase();

    public void SaveSuggestions()
    {
        //open a new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(SuggestionDatabase));
        FileStream stream = new FileStream(globalSuggestionFilePath, FileMode.Create, FileAccess.ReadWrite);
        serializer.Serialize(stream, suggestionDB);
        stream.Close();
    }

    public void LoadSuggestions()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SuggestionDatabase));
        if (File.Exists(globalSuggestionFilePath))
        {
            FileStream stream = new FileStream(globalSuggestionFilePath, FileMode.Open, FileAccess.ReadWrite);
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

    //
    //All code regarding saving and loading questions
    //


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
    public string[] answers;
}

[Serializable]
public class QuestionDatabase
{
    public List<QuestionEntry> list = new List<QuestionEntry>();
}