using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla un item individual de Kahoot en la lista
/// </summary>
public class KahootItemUI : MonoBehaviour
{
    public TMP_Text kahootNameText;
    public TMP_Text descriptionText;
    public Button playButton;
    public Button highscoresButton;

    private Khajot kahoot;

    /// <summary>
    /// Inicializa el item con datos del Kahoot
    /// </summary>
    public void Initialize(Khajot kahootData)
    {
        kahoot = kahootData;
        kahootNameText.text = kahoot.khajotName;
        descriptionText.text = kahoot.description;

        playButton.onClick.AddListener(OnPlayClicked);
        highscoresButton.onClick.AddListener(OnHighscoresClicked);
    }

    /// <summary>
    /// Cuando se hace clic en Jugar
    /// </summary>
    private void OnPlayClicked()
    {
        GameManager.Instance.StartKahoot(kahoot);
    }

    /// <summary>
    /// Cuando se hace clic en Ver Highscores
    /// </summary>
    private void OnHighscoresClicked()
    {
        // Guardar el Kahoot seleccionado para ver sus highscores
        PlayerPrefs.SetString("SelectedKahoot", kahoot.khajotName);
        SceneController.LoadLeaderboard();
    }
}