// AllScoresController.cs
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla la escena que muestra todos los scores de todos los kahoots
/// </summary>
public class AllScoresController : MonoBehaviour
{
    public Transform scoresContainer;
    public GameObject scoreItemPrefab;
    public Button backButton;
    public TMP_Text titleText;

    /// <summary>
    /// Inicializa la escena de todos los scores
    /// </summary>
    void Start()
    {
        backButton.onClick.AddListener(OnBackClicked);
        titleText.text = "TODOS LOS SCORES";

        // Cargar todos los scores
        LoadAllScores();
    }

    /// <summary>
    /// Carga y muestra todos los scores de todos los kahoots
    /// </summary>
    private void LoadAllScores()
    {
        // Obtener todos los leaderboards
        Dictionary<string, Leaderboard> allLeaderboards = XMLManager.GetAllLeaderboards();

        int position = 1;

        // Recorrer cada kahoot
        foreach (var kvp in allLeaderboards)
        {
            string kahootName = kvp.Key;
            Leaderboard leaderboard = kvp.Value;

            // Mostrar cada entrada de este leaderboard
            foreach (LeaderboardEntry entry in leaderboard.entries)
            {
                GameObject itemObj = Instantiate(scoreItemPrefab, scoresContainer);
                ScoreItemUI itemUI = itemObj.GetComponent<ScoreItemUI>();

                if (itemUI != null)
                {
                    itemUI.Initialize(position, entry, kahootName);
                    position++;
                }
            }
        }
    }

    /// <summary>
    /// Cuando se hace clic en Atrás
    /// </summary>
    private void OnBackClicked()
    {
        SceneController.LoadMainMenu();
    }
}