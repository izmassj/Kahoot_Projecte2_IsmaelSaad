using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla la escena del menú principal
/// </summary>
public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Button highscoresButton;
    public Button reportsButton;
    public Button aboutButton;
    public Button exitButton;

    /// <summary>
    /// Inicializa los botones del menú principal
    /// </summary>
    void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        highscoresButton.onClick.AddListener(OnHighscoresClicked);
        reportsButton.onClick.AddListener(OnReportsClicked);
        aboutButton.onClick.AddListener(OnAboutClicked);
        exitButton.onClick.AddListener(OnExitClicked);

        // Cargar nombre de usuario si existe
        if (PlayerPrefs.HasKey("UserName"))
        {
            GameManager.Instance.SetUserName(PlayerPrefs.GetString("UserName"));
        }
    }

    /// <summary>
    /// Cuando se hace clic en Jugar
    /// </summary>
    private void OnPlayClicked()
    {
        SceneController.LoadPlaySelector();
    }

    /// <summary>
    /// Cuando se hace clic en Highscores
    /// </summary>
    private void OnHighscoresClicked()
    {
        SceneController.LoadLeaderboardAll();
    }

    /// <summary>
    /// Cuando se hace clic en Informes
    /// </summary>
    private void OnReportsClicked()
    {
        SceneController.LoadReportSelection();
    }

    /// <summary>
    /// Cuando se hace clic en About
    /// </summary>
    private void OnAboutClicked()
    {
        SceneController.LoadAbout();
    }

    /// <summary>
    /// Cuando se hace clic en Salir
    /// </summary>
    private void OnExitClicked()
    {
        Application.Quit();
    }
}