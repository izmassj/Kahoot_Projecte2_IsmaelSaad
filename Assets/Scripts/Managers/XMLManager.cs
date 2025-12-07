using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maneja la lectura y escritura de archivos XML para leaderboards
/// </summary>
public static class XMLManager
{
    private static string leaderboardsPath;

    /// <summary>
    /// Inicializa el gestor de XML
    /// </summary>
    public static void Initialize()
    {
        leaderboardsPath = Path.Combine(Application.persistentDataPath, "Leaderboards");

        if (!Directory.Exists(leaderboardsPath))
        {
            Directory.CreateDirectory(leaderboardsPath);
        }
    }

    /// <summary>
    /// Obtiene el nombre del archivo XML para un Kahoot específico
    /// </summary>
    private static string GetLeaderboardFileName(string kahootName)
    {
        string safeName = kahootName.Replace(" ", "_").Replace("?", "");
        return $"{safeName}_leaderboard.xml";
    }

    /// <summary>
    /// Guarda una nueva entrada en el leaderboard
    /// </summary>
    public static void SaveScore(string kahootName, string userName, int score, float time)
    {
        try
        {
            string fileName = GetLeaderboardFileName(kahootName);
            string filePath = Path.Combine(leaderboardsPath, fileName);

            Leaderboard leaderboard;

            // Cargar leaderboard existente o crear uno nuevo
            if (File.Exists(filePath))
            {
                leaderboard = LoadLeaderboard(filePath);
            }
            else
            {
                leaderboard = new Leaderboard();
            }

            // Agregar nueva entrada
            LeaderboardEntry newEntry = new LeaderboardEntry
            {
                userName = userName,
                score = score,
                time = time,
                date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            leaderboard.entries.Add(newEntry);

            // Ordenar por puntuación descendente
            leaderboard.entries.Sort((a, b) => b.score.CompareTo(a.score));

            // Guardar en XML
            XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, leaderboard);
            }
        }
        catch (Exception ex)
        {
            ExceptionHandler.SaveExceptionToFile($"Error al guardar score para: {kahootName}", ex.Message);
        }
    }

    /// <summary>
    /// Carga un leaderboard desde archivo XML
    /// </summary>
    public static Leaderboard LoadLeaderboard(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return (Leaderboard)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.SaveExceptionToFile($"Error al cargar leaderboard: {filePath}", ex.Message);
                return new Leaderboard();
            }
        }
        return new Leaderboard();
    }

    /// <summary>
    /// Carga el leaderboard para un Kahoot específico
    /// </summary>
    public static Leaderboard LoadKahootLeaderboard(string kahootName)
    {
        string fileName = GetLeaderboardFileName(kahootName);
        string filePath = Path.Combine(leaderboardsPath, fileName);

        return LoadLeaderboard(filePath);
    }

    /// <summary>
    /// Obtiene todos los leaderboards disponibles
    /// </summary>
    public static Dictionary<string, Leaderboard> GetAllLeaderboards()
    {
        Dictionary<string, Leaderboard> allLeaderboards = new Dictionary<string, Leaderboard>();

        if (Directory.Exists(leaderboardsPath))
        {
            string[] xmlFiles = Directory.GetFiles(leaderboardsPath, "*.xml");

            foreach (string file in xmlFiles)
            {
                try
                {
                    Leaderboard leaderboard = LoadLeaderboard(file);
                    string kahootName = Path.GetFileName(file).Replace("_leaderboard.xml", "").Replace("_", " ");
                    allLeaderboards[kahootName] = leaderboard;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.SaveExceptionToFile($"Error al cargar leaderboard: {file}", ex.Message);
                }
            }
        }

        return allLeaderboards;
    }
}