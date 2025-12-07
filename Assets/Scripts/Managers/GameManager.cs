// GameManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase principal que gestiona el flujo del juego Kahoot entre múltiples escenas
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Variables del juego persistentes entre escenas
    private string currentUserName = "Usuario";
    private Khajot currentKahoot;
    private int currentScore = 0;
    private List<bool> playerAnswers = new List<bool>();
    private float gameStartTime;

    // Lista de todos los Kahoots disponibles
    private List<Khajot> allKahoots = new List<Khajot>();

    /// <summary>
    /// Inicializa el singleton y los sistemas
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Inicializar sistemas
            ExceptionHandler.Initialize();
            JSONManager.Initialize();
            XMLManager.Initialize();

            // Cargar todos los Kahoots disponibles
            LoadAllKahoots();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Carga todos los Kahoots disponibles
    /// </summary>
    private void LoadAllKahoots()
    {
        allKahoots = JSONManager.LoadAllKahoots();
    }

    /// <summary>
    /// Obtiene la lista de Kahoots disponibles
    /// </summary>
    public List<Khajot> GetAvailableKahoots()
    {
        return allKahoots;
    }

    /// <summary>
    /// Inicia un Kahoot específico
    /// </summary>
    public void StartKahoot(Khajot kahoot)
    {
        currentKahoot = kahoot;
        currentScore = 0;
        playerAnswers.Clear();
        gameStartTime = Time.time;

        // Guardar el Kahoot seleccionado
        PlayerPrefs.SetString("SelectedKahoot", kahoot.khajotName);

        // Cargar escena del juego
        SceneController.LoadKahootGame();
    }

    /// <summary>
    /// Procesa la respuesta del jugador
    /// </summary>
    public void AnswerQuestion(int answerIndex, float timeRemaining)
    {
        if (currentKahoot == null)
            return;

        // Calcular puntuación basada en tiempo restante
        int pointsEarned = Mathf.Max(10, Mathf.RoundToInt(timeRemaining * 10));
        currentScore += pointsEarned;

        // Guardar si la respuesta fue correcta
        // Nota: Necesitarías tener acceso a la pregunta actual
    }

    /// <summary>
    /// Finaliza el juego y guarda la puntuación
    /// </summary>
    public void EndGame(int finalScore)
    {
        if (currentKahoot != null && !string.IsNullOrEmpty(currentUserName))
        {
            float totalTime = Time.time - gameStartTime;
            XMLManager.SaveScore(currentKahoot.khajotName, currentUserName, finalScore, totalTime);
        }

        // Cargar escena de highscores
        SceneController.LoadLeaderboard();
    }

    /// <summary>
    /// Establece el nombre de usuario
    /// </summary>
    public void SetUserName(string userName)
    {
        if (!string.IsNullOrEmpty(userName))
        {
            currentUserName = userName;
            PlayerPrefs.SetString("UserName", userName);
        }
    }

    /// <summary>
    /// Obtiene el nombre de usuario actual
    /// </summary>
    public string GetUserName()
    {
        return currentUserName;
    }

    /// <summary>
    /// Crea un nuevo Kahoot
    /// </summary>
    public void CreateNewKahoot(string name, string description, List<Question> questions)
    {
        Khajot newKahoot = new Khajot
        {
            khajotName = name,
            description = description,
            questions = questions
        };

        bool success = JSONManager.SaveKahoot(newKahoot);

        if (success)
        {
            // Recargar la lista de Kahoots
            LoadAllKahoots();

            // Volver al selector de Kahoots
            SceneController.LoadKahootSelector();
        }
    }

    /// <summary>
    /// Obtiene el Kahoot actual
    /// </summary>
    public Khajot GetCurrentKahoot()
    {
        return currentKahoot;
    }

    /// <summary>
    /// Obtiene la puntuación actual
    /// </summary>
    public int GetCurrentScore()
    {
        return currentScore;
    }

    /// <summary>
    /// Reinicia los datos del juego actual
    /// </summary>
    public void ResetGameData()
    {
        currentKahoot = null;
        currentScore = 0;
        playerAnswers.Clear();
    }
}