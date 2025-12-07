using System;
using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// Modelo para una pregunta del Kahoot
/// </summary>
[System.Serializable]
public class Question
{
    public string question;
    public int time;
    public string image;
    public List<string> answers;
    public int rightAnswerIndex;
}

/// <summary>
/// Modelo completo de un Kahoot
/// </summary>
[System.Serializable]
public class Khajot
{
    public string khajotName;
    public string description;
    public List<Question> questions;
}

/// <summary>
/// Modelo para una entrada en el leaderboard
/// </summary>
[System.Serializable]
[XmlRoot("LeaderboardEntry")]
public class LeaderboardEntry
{
    [XmlElement("UserName")]
    public string userName;

    [XmlElement("Score")]
    public int score;

    [XmlElement("Time")]
    public float time;

    [XmlElement("Date")]
    public string date;
}

/// <summary>
/// Modelo para el leaderboard completo
/// </summary>
[XmlRoot("Leaderboard")]
public class Leaderboard
{
    [XmlArray("Entries")]
    [XmlArrayItem("Entry")]
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}