using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maneja la lectura y escritura de archivos JSON
/// </summary>
public static class JSONManager
{
    private static string defaultKahootsPath;
    private static string customKahootsPath;

    /// <summary>
    /// Inicializa las rutas de los archivos JSON
    /// </summary>
    public static void Initialize()
    {
        defaultKahootsPath = Path.Combine(Application.dataPath, "LocalKhajots");
        customKahootsPath = Path.Combine(Application.persistentDataPath, "CustomKahoots");

        // Crear directorios si no existen
        if (!Directory.Exists(customKahootsPath))
        {
            Directory.CreateDirectory(customKahootsPath);
        }
    }

    /// <summary>
    /// Carga todos los Kahoots disponibles
    /// </summary>
    public static List<Khajot> LoadAllKahoots()
    {
        List<Khajot> allKahoots = new List<Khajot>();

        // Cargar Kahoots por defecto
        LoadKahootsFromPath(defaultKahootsPath, allKahoots);

        // Cargar Kahoots personalizados
        LoadKahootsFromPath(customKahootsPath, allKahoots);

        return allKahoots;
    }

    /// <summary>
    /// Carga Kahoots desde una ruta específica
    /// </summary>
    private static void LoadKahootsFromPath(string path, List<Khajot> kahootList)
    {
        if (Directory.Exists(path))
        {
            string[] jsonFiles = Directory.GetFiles(path, "*.json");

            foreach (string file in jsonFiles)
            {
                try
                {
                    Khajot kahoot = LoadKahoot(file);
                    if (kahoot != null)
                    {
                        kahootList.Add(kahoot);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.SaveExceptionToFile($"Error al cargar JSON: {file}", ex.Message);
                }
            }
        }
    }

    /// <summary>
    /// Carga un Kahoot específico desde un archivo
    /// </summary>
    public static Khajot LoadKahoot(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            return JsonUtility.FromJson<Khajot>(jsonContent);
        }
        return null;
    }

    /// <summary>
    /// Guarda un nuevo Kahoot como archivo JSON
    /// </summary>
    public static bool SaveKahoot(Khajot kahoot)
    {
        try
        {
            string fileName = $"{kahoot.khajotName.Replace(" ", "_")}.json";
            string filePath = Path.Combine(customKahootsPath, fileName);

            string jsonContent = JsonUtility.ToJson(kahoot, true);
            File.WriteAllText(filePath, jsonContent);

            return true;
        }
        catch (Exception ex)
        {
            ExceptionHandler.SaveExceptionToFile($"Error al guardar Kahoot: {kahoot.khajotName}", ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Obtiene la ruta de los Kahoots personalizados
    /// </summary>
    public static string GetCustomKahootsPath()
    {
        return customKahootsPath;
    }
}