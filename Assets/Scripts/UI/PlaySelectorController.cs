using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla la escena de selección de juego (Main Menu extendido)
/// </summary>
public class PlaySelectorController : MonoBehaviour
{
    [Header("Botones de Navegación")]
    public Button backButton;
    public Button viewKahootsButton;
    public Button createKahootsButton;

    /// <summary>
    /// Inicializa los botones del selector
    /// </summary>
    void Start()
    {
        // Configurar listeners para los botones
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackClicked);
        }

        if (viewKahootsButton != null)
        {
            viewKahootsButton.onClick.AddListener(OnViewKahootsClicked);
        }

        if (createKahootsButton != null)
        {
            createKahootsButton.onClick.AddListener(OnCreateKahootsClicked);
        }
    }

    /// <summary>
    /// Cuando se hace clic en el botón Atrás
    /// </summary>
    private void OnBackClicked()
    {
        SceneController.LoadMainMenu();
    }

    /// <summary>
    /// Cuando se hace clic en Ver Kahoots disponibles
    /// </summary>
    private void OnViewKahootsClicked()
    {
        SceneController.LoadKahootSelector();
    }

    /// <summary>
    /// Cuando se hace clic en Crear Kahoots
    /// </summary>
    private void OnCreateKahootsClicked()
    {
        SceneController.LoadKahootCreator();
    }
}