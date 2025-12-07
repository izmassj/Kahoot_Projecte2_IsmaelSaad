using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla la escena del visor de informes
/// </summary>
public class ReportViewerController : MonoBehaviour
{
    public TMP_Text reportContentText;
    public Button backButton;
    public Button deleteButton;
    public TMP_Text reportTitleText;

    private string currentReportPath;

    /// <summary>
    /// Inicializa el visor de informes
    /// </summary>
    void Start()
    {
        backButton.onClick.AddListener(OnBackClicked);
        deleteButton.onClick.AddListener(OnDeleteClicked);

        LoadReport();
    }

    /// <summary>
    /// Carga el informe seleccionado
    /// </summary>
    private void LoadReport()
    {
        currentReportPath = SceneController.GetSelectedReport();

        if (!string.IsNullOrEmpty(currentReportPath))
        {
            reportTitleText.text = System.IO.Path.GetFileName(currentReportPath);
            string content = ExceptionHandler.ReadReport(currentReportPath);
            reportContentText.text = content;
        }
        else
        {
            reportTitleText.text = "Informe no disponible";
            reportContentText.text = "No se pudo cargar el informe seleccionado.";
        }
    }

    /// <summary>
    /// Cuando se hace clic en Atrás
    /// </summary>
    private void OnBackClicked()
    {
        SceneController.LoadReportSelection();
    }

    /// <summary>
    /// Cuando se hace clic en Eliminar
    /// </summary>
    private void OnDeleteClicked()
    {
        if (!string.IsNullOrEmpty(currentReportPath) && System.IO.File.Exists(currentReportPath))
        {
            System.IO.File.Delete(currentReportPath);
            SceneController.LoadReportSelection();
        }
    }
}