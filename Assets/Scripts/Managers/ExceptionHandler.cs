using System;
using System.IO;
using UnityEngine;

public class ExceptionHandler : MonoBehaviour
{
    private static string reportsPath;

    /// <summary>
    /// Inicializa el manejador de excepciones
    /// </summary>
    public static void Initialize()
    {
        reportsPath = Path.Combine(Application.persistentDataPath, "ExceptionReports");

        if (!Directory.Exists(reportsPath))
        {
            Directory.CreateDirectory(reportsPath);
        }

        // Suscribirse al evento de excepciones no manejadas
        Application.logMessageReceived += HandleException;
    }

    /// <summary>
    /// Maneja las excepciones y las guarda en archivos
    /// </summary>
    private static void HandleException(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Exception || type == LogType.Error)
        {
            SaveExceptionToFile(condition, stackTrace);
        }
    }

    /// <summary>
    /// Guarda una excepción en un archivo con fecha y hora
    /// </summary>
    public static void SaveExceptionToFile(string error, string stackTrace = "")
    {
        try
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"Exception_{timestamp}.txt";
            string filePath = Path.Combine(reportsPath, fileName);

            string content = $"Fecha: {DateTime.Now}\n" +
                           $"Error: {error}\n" +
                           $"StackTrace: {stackTrace}\n" +
                           $"--- Fin del reporte ---";

            File.WriteAllText(filePath, content);
            Debug.Log($"Excepción guardada en: {filePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al guardar excepción: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene la lista de archivos de reportes disponibles
    /// </summary>
    public static string[] GetReportFiles()
    {
        if (Directory.Exists(reportsPath))
        {
            return Directory.GetFiles(reportsPath, "*.txt");
        }
        return new string[0];
    }

    /// <summary>
    /// Lee el contenido de un archivo de reporte
    /// </summary>
    public static string ReadReport(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        return "Archivo no encontrado";
    }
}
