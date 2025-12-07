using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla la navegación entre escenas del juego Kahoot
/// </summary>
public static class SceneController
{
    // Nombres de las escenas (deben coincidir con los nombres en Build Settings)
    public const string MAIN_MENU = "MainMenu";
    public const string KAHOOT_SELECTOR = "KhajotSelector";
    public const string PLAY_SELECTOR = "PlaySelector";
    public const string KAHOOT_GAME = "KhajotGame";
    public const string LEADERBOARD = "Leaderboard";
    public const string LEADERBOARD_ALL = "LeaderboardAll";
    public const string REPORT_SELECTION = "ReportSelector";
    public const string ABOUT = "About";
    public const string REPORT_VIEWER = "ReportViewer";
    public const string KAHOOT_CREATOR = "KhajotCreator";

    /// <summary>
    /// Carga la escena del menú principal
    /// </summary>
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU);
    }

    /// <summary>
    /// Carga la escena del selector de Kahoots
    /// </summary>
    public static void LoadKahootSelector()
    {
        SceneManager.LoadScene(KAHOOT_SELECTOR);
    }
    /// <summary>
    /// Carga la escena del selector de modos
    /// </summary>
    public static void LoadPlaySelector()
    {
        SceneManager.LoadScene(PLAY_SELECTOR);
    }


    /// <summary>
    /// Carga la escena del juego con un Kahoot específico
    /// </summary>
    public static void LoadKahootGame(string kahootName = "")
    {
        // Guardar el nombre del Kahoot seleccionado
        if (!string.IsNullOrEmpty(kahootName))
        {
            PlayerPrefs.SetString("SelectedKahoot", kahootName);
        }
        SceneManager.LoadScene(KAHOOT_GAME);
    }

    /// <summary>
    /// Carga la escena de highscores
    /// </summary>
    public static void LoadLeaderboard()
    {
        SceneManager.LoadScene(LEADERBOARD);
    }

    /// <summary>
    /// Carga la escena de highscores para todos los Khajots
    /// </summary>
    public static void LoadLeaderboardAll()
    {
        SceneManager.LoadScene(LEADERBOARD_ALL);
    }

    /// <summary>
    /// Carga la escena de selección de informes
    /// </summary>
    public static void LoadReportSelection()
    {
        SceneManager.LoadScene(REPORT_SELECTION);
    }

    /// <summary>
    /// Carga la escena About
    /// </summary>
    public static void LoadAbout()
    {
        SceneManager.LoadScene(ABOUT);
    }

    /// <summary>
    /// Carga la escena del visor de informes
    /// </summary>
    public static void LoadReportViewer(string reportFilePath = "")
    {
        if (!string.IsNullOrEmpty(reportFilePath))
        {
            PlayerPrefs.SetString("SelectedReport", reportFilePath);
        }
        SceneManager.LoadScene(REPORT_VIEWER);
    }

    /// <summary>
    /// Carga la escena del creador de Kahoots
    /// </summary>
    public static void LoadKahootCreator()
    {
        SceneManager.LoadScene(KAHOOT_CREATOR);
    }

    /// <summary>
    /// Obtiene el Kahoot seleccionado actualmente
    /// </summary>
    public static string GetSelectedKahoot()
    {
        return PlayerPrefs.GetString("SelectedKahoot", "");
    }

    /// <summary>
    /// Obtiene el informe seleccionado actualmente
    /// </summary>
    public static string GetSelectedReport()
    {
        return PlayerPrefs.GetString("SelectedReport", "");
    }
}