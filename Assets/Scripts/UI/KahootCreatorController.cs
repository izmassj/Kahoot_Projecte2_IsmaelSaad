using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla la interfaz de usuario para crear nuevos Kahoots
/// </summary>
public class KahootCreatorUI : MonoBehaviour
{
    public TMP_InputField kahootNameInput;
    public TMP_InputField descriptionInput;
    public Transform questionsContainer;
    public GameObject questionPrefab;
    public Button addQuestionButton;
    public Button saveButton;
    public Button backButton;

    private List<QuestionUI> questionUIs = new List<QuestionUI>();

    /// <summary>
    /// Inicializa el creador
    /// </summary>
    void Start()
    {
        addQuestionButton.onClick.AddListener(AddQuestion);
        saveButton.onClick.AddListener(SaveKahoot);
        backButton.onClick.AddListener(OnBackClicked);

        // Agregar primera pregunta por defecto
        AddQuestion();
    }

    /// <summary>
    /// Agrega una nueva pregunta al creador
    /// </summary>
    public void AddQuestion()
    {
        GameObject questionObj = Instantiate(questionPrefab, questionsContainer);
        QuestionUI questionUI = questionObj.GetComponent<QuestionUI>();

        if (questionUI != null)
        {
            questionUI.Initialize(this, questionUIs.Count);
            questionUIs.Add(questionUI);
        }
    }

    /// <summary>
    /// Elimina una pregunta de la lista
    /// </summary>
    public void RemoveQuestion(QuestionUI questionUI)
    {
        if (questionUIs.Contains(questionUI))
        {
            questionUIs.Remove(questionUI);

            // Reindexar las preguntas restantes
            for (int i = 0; i < questionUIs.Count; i++)
            {
                questionUIs[i].SetQuestionIndex(i);
            }
        }
    }

    /// <summary>
    /// Guarda el Kahoot creado
    /// </summary>
    private void SaveKahoot()
    {
        string name = kahootNameInput.text;
        string description = descriptionInput.text;

        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("El nombre del Kahoot es obligatorio");
            return;
        }

        List<Question> questions = new List<Question>();

        foreach (QuestionUI questionUI in questionUIs)
        {
            Question question = questionUI.GetQuestion();
            if (question != null)
            {
                questions.Add(question);
            }
        }

        if (questions.Count == 0)
        {
            Debug.LogError("Debe agregar al menos una pregunta");
            return;
        }

        GameManager.Instance.CreateNewKahoot(name, description, questions);
    }

    private void OnBackClicked()
    {
        SceneController.LoadPlaySelector();
    }
}

