using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;

    private void Awake()
    {
        instance = this;
    }

    public HighscoreDatabase db = new HighscoreDatabase();

    //save
    public void SaveHighscores()
    {
        //open a new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(HighscoreDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/highscores.xml", FileMode.Create);
        serializer.Serialize(stream, db);
        stream.Close();
    }

    //load
    public void LoadHighscores()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(HighscoreDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/highscores.xml", FileMode.Open);
        db = serializer.Deserialize(stream) as HighscoreDatabase;
        stream.Close();
    }

    public void AddHighscore(string username, string highscore)
    {
        HighscoreEntry entry = new HighscoreEntry();
        entry.username = username;
        entry.highscore = highscore;
        db.list.Add(entry);
    }

    public void ModifyHighscore(string username, string highscore)
    {
        int index = db.list.FindIndex(item => item.username == username);
        if(int.Parse(highscore) > int.Parse(db.list[index].highscore))
        {
            db.list[index].highscore = highscore;
        }
    }

}
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
