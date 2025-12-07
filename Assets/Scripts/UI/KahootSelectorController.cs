using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Controla la escena del selector de Kahoots
/// </summary>
public class KahootSelectorController : MonoBehaviour
{
    public Transform kahootListContainer;
    public GameObject kahootItemPrefab;
    public Button backButton;
    //public Button createButton;
    public TMP_InputField userNameInput;
    public Button saveUserNameButton;

    private List<KahootItemUI> kahootItems = new List<KahootItemUI>();

    /// <summary>
    /// Inicializa el selector de Kahoots
    /// </summary>
    void Start()
    {
        backButton.onClick.AddListener(OnBackClicked);
        //createButton.onClick.AddListener(OnCreateClicked);
        saveUserNameButton.onClick.AddListener(OnSaveUserNameClicked);

        // Cargar nombre de usuario actual
        userNameInput.text = GameManager.Instance.GetUserName();

        // Cargar lista de Kahoots
        LoadLocalKahoots();
        LoadKahoots();
    }

    private void LoadLocalKahoots()
    {
        // Limpiar lista actual
        foreach (Transform child in kahootListContainer)
        {
            Destroy(child.gameObject);
        }
        kahootItems.Clear();

        // Obtener Kahoots del GameManager
        List<Khajot> kahoots = GameManager.Instance.GetAvailableKahoots();

        // Crear items para cada Kahoot
        foreach (Khajot kahoot in kahoots)
        {
            GameObject itemObj = Instantiate(kahootItemPrefab, kahootListContainer);
            KahootItemUI itemUI = itemObj.GetComponent<KahootItemUI>();

            if (itemUI != null)
            {
                itemUI.Initialize(kahoot);
                kahootItems.Add(itemUI);
            }
        }
    }

    /// <summary>
    /// Carga y muestra la lista de Kahoots disponibles
    /// </summary>
    private void LoadKahoots()
    {
        // Limpiar lista actual
        foreach (Transform child in kahootListContainer)
        {
            Destroy(child.gameObject);
        }
        kahootItems.Clear();

        // Obtener Kahoots del GameManager
        List<Khajot> kahoots = GameManager.Instance.GetAvailableKahoots();

        // Crear items para cada Kahoot
        foreach (Khajot kahoot in kahoots)
        {
            GameObject itemObj = Instantiate(kahootItemPrefab, kahootListContainer);
            KahootItemUI itemUI = itemObj.GetComponent<KahootItemUI>();

            if (itemUI != null)
            {
                itemUI.Initialize(kahoot);
                kahootItems.Add(itemUI);
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
    /// Cuando se hace clic en Crear
    /// </summary>
    private void OnCreateClicked()
    {
        SceneController.LoadKahootCreator();
    }

    /// <summary>
    /// Cuando se guarda el nombre de usuario
    /// </summary>
    private void OnSaveUserNameClicked()
    {
        GameManager.Instance.SetUserName(userNameInput.text);
    }
}
