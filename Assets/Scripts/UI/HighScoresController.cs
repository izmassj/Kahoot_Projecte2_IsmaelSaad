using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla la escena de highscores
/// </summary>
public class HighscoresController : MonoBehaviour
{
    public Transform highscoresContainer;
    public GameObject scoreItemPrefab;
    public Button backButton;
    public TMP_Text kahootNameText;

    /// <summary>
    /// Inicializa la escena de highscores
    /// </summary>
    void Start()
    {
        backButton.onClick.AddListener(OnBackClicked);

        // Cargar highscores
        LoadHighscores();
    }

    /// <summary>
    /// Carga y muestra los highscores
    /// </summary>
    private void LoadHighscores()
    {
        string selectedKahoot = SceneController.GetSelectedKahoot();

        if (!string.IsNullOrEmpty(selectedKahoot))
        {
            kahootNameText.text = selectedKahoot;

            // Cargar leaderboard desde XML
            Leaderboard leaderboard = XMLManager.LoadKahootLeaderboard(selectedKahoot);

            // Mostrar cada entrada
            int position = 1;
            foreach (LeaderboardEntry entry in leaderboard.entries)
            {
                GameObject itemObj = Instantiate(scoreItemPrefab, highscoresContainer);
                ScoreItemUI itemUI = itemObj.GetComponent<ScoreItemUI>();

                if (itemUI != null)
                {
                    itemUI.Initialize(position, entry);
                    position++;
                }
            }
        }
        else
        {
            kahootNameText.text = "Selecciona un Kahoot";
        }
    }

    /// <summary>
    /// Cuando se hace clic en Atrás
    /// </summary>
    private void OnBackClicked()
    {
        SceneController.LoadKahootSelector();
    }
}
