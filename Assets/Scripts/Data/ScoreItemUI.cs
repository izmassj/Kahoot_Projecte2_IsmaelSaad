// ScoreItemUI.cs (actualizado)
using TMPro;
using UnityEngine;

public class ScoreItemUI : MonoBehaviour
{
    public TMP_Text positionText;
    public TMP_Text userNameText;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text dateText;
    public TMP_Text kahootNameText; // Nuevo campo para mostrar el nombre del kahoot

    /// <summary>
    /// Inicializa el item con datos del score
    /// </summary>
    public void Initialize(int position, LeaderboardEntry entry, string kahootName = "")
    {
        positionText.text = position.ToString();
        userNameText.text = entry.userName;
        scoreText.text = entry.score.ToString();
        timeText.text = entry.time.ToString("F2") + "s";
        dateText.text = entry.date;

        // Mostrar nombre del kahoot si se proporciona
        if (!string.IsNullOrEmpty(kahootName))
        {
            kahootNameText.text = kahootName;
            kahootNameText.gameObject.SetActive(true);
        }
    }
}