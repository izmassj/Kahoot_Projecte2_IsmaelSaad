using TMPro;
using UnityEngine;

/// <summary>
/// Controla un item individual de score en la lista completa
/// </summary>
public class AllScoresItemUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text positionText;
    public TMP_Text userNameText;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text dateText;

    /// <summary>
    /// Inicializa el item con datos del score
    /// </summary>
    public void Initialize(int position, LeaderboardEntry entry)
    {
        positionText.text = $"{position}.";
        userNameText.text = entry.userName;
        scoreText.text = entry.score.ToString();
        timeText.text = $"{entry.time:F1}s";

        // Formatear fecha (si solo tiene fecha, no hora)
        if (entry.date.Length > 10)
        {
            dateText.text = entry.date.Substring(0, 10); // Solo fecha
        }
        else
        {
            dateText.text = entry.date;
        }
    }
}