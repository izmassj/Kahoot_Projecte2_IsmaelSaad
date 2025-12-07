using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Controla la escena de selección de informes
/// </summary>
public class ReportSelectionController : MonoBehaviour
{
    public Transform reportsListContainer;
    public GameObject reportItemPrefab;
    public Button backButton;
    public Button refreshButton;
    public TMP_Text noReportsText;

    private List<ReportItemUI> reportItems = new List<ReportItemUI>();

    /// <summary>
    /// Inicializa la escena de selección de informes
    /// </summary>
    void Start()
    {
        backButton.onClick.AddListener(OnBackClicked);
        refreshButton.onClick.AddListener(OnRefreshClicked);

        LoadReports();
    }

    /// <summary>
    /// Carga la lista de informes disponibles
    /// </summary>
    private void LoadReports()
    {
        // Limpiar lista actual
        foreach (Transform child in reportsListContainer)
        {
            Destroy(child.gameObject);
        }
        reportItems.Clear();

        // Obtener informes
        string[] reportFiles = ExceptionHandler.GetReportFiles();

        if (reportFiles.Length == 0)
        {
            noReportsText.gameObject.SetActive(true);
            return;
        }

        noReportsText.gameObject.SetActive(false);

        // Crear items para cada informe
        foreach (string reportFile in reportFiles)
        {
            GameObject itemObj = Instantiate(reportItemPrefab, reportsListContainer);
            ReportItemUI itemUI = itemObj.GetComponent<ReportItemUI>();

            if (itemUI != null)
            {
                itemUI.Initialize(reportFile);
                reportItems.Add(itemUI);
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

    /// <summary>
    /// Cuando se hace clic en Actualizar
    /// </summary>
    private void OnRefreshClicked()
    {
        LoadReports();
    }
}
