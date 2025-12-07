using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla la escena About
/// </summary>
public class AboutController : MonoBehaviour
{
    public Button backButton;
    public TMP_Text creatorText;
    public TMP_Text versionText;

    /// <summary>
    /// Inicializa la escena About
    /// </summary>
    void Start()
    {
        backButton.onClick.AddListener(OnBackClicked);

        // Mostrar información de la aplicación
        versionText.text = "Versión 1.0";
        creatorText.text = "Desarrollado por [Tu Nombre]";
    }

    /// <summary>
    /// Cuando se hace clic en Atrás
    /// </summary>
    private void OnBackClicked()
    {
        SceneController.LoadMainMenu();
    }
}