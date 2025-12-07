using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla un item individual de informe
/// </summary>
public class ReportItemUI : MonoBehaviour
{
    public TMP_Text reportNameText;
    public Button viewButton;

    private string reportFilePath;

    /// <summary>
    /// Inicializa el item con datos del informe
    /// </summary>
    public void Initialize(string filePath)
    {
        reportFilePath = filePath;
        reportNameText.text = System.IO.Path.GetFileName(filePath);

        viewButton.onClick.AddListener(OnViewClicked);
    }

    /// <summary>
    /// Cuando se hace clic en Ver
    /// </summary>
    private void OnViewClicked()
    {
        SceneController.LoadReportViewer(reportFilePath);
    }
}