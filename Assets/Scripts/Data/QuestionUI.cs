using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla la interfaz de una pregunta individual
/// </summary>
public class QuestionUI : MonoBehaviour
{
    public TMP_InputField questionText;
    public TMP_InputField timeInput;
    public TMP_InputField imagePathInput;
    public TMP_InputField[] answerInputs;
    public TMP_Dropdown correctAnswerDropdown;
    public Button deleteButton; // Botón para borrar la pregunta

    private KahootCreatorUI creatorUI; // Referencia al creador principal
    private int questionIndex; // Índice de esta pregunta

    /// <summary>
    /// Inicializa la UI de la pregunta con referencia al creador
    /// </summary>
    public void Initialize(KahootCreatorUI creator, int index)
    {
        creatorUI = creator;
        questionIndex = index;

        questionText.text = $"Pregunta {index + 1}";
        timeInput.text = "20";

        // Configurar opciones del dropdown
        correctAnswerDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 1; i <= 4; i++)
        {
            options.Add($"Respuesta {i}");
        }
        correctAnswerDropdown.AddOptions(options);

        // Configurar el botón de borrar
        if (deleteButton != null)
        {
            deleteButton.onClick.RemoveAllListeners();
            deleteButton.onClick.AddListener(DeleteThisQuestion);
        }
    }

    /// <summary>
    /// Borra esta pregunta del creador
    /// </summary>
    private void DeleteThisQuestion()
    {
        if (creatorUI != null)
        {
            creatorUI.RemoveQuestion(this);
        }

        // Destruir este GameObject
        Destroy(gameObject);
    }

    /// <summary>
    /// Obtiene la pregunta desde la UI, recolectando solo los strings de las respuestas
    /// </summary>
    public Question GetQuestion()
    {
        if (string.IsNullOrEmpty(questionText.text))
        {
            Debug.LogError("El texto de la pregunta es obligatorio");
            return null;
        }

        // Crear la lista de strings para las respuestas
        List<string> answerStrings = new List<string>();

        // Recolectar solo los strings de los InputFields de respuestas
        for (int i = 0; i < answerInputs.Length; i++)
        {
            if (!string.IsNullOrEmpty(answerInputs[i].text))
            {
                answerStrings.Add(answerInputs[i].text);
            }
            else
            {
                // Si un campo está vacío, agregar una cadena vacía para mantener la estructura
                answerStrings.Add("");
            }
        }

        // Verificar que haya al menos una respuesta no vacía
        bool hasValidAnswer = false;
        foreach (string answer in answerStrings)
        {
            if (!string.IsNullOrEmpty(answer))
            {
                hasValidAnswer = true;
                break;
            }
        }

        if (!hasValidAnswer)
        {
            Debug.LogError("Debe agregar al menos una respuesta");
            return null;
        }

        // Crear y devolver el objeto Question
        Question question = new Question
        {
            question = questionText.text,
            time = int.Parse(timeInput.text),
            image = imagePathInput.text,
            answers = answerStrings,
            rightAnswerIndex = correctAnswerDropdown.value
        };

        return question;
    }

    /// <summary>
    /// Establece el índice de la pregunta
    /// </summary>
    public void SetQuestionIndex(int index)
    {
        questionIndex = index;
        questionText.text = $"Pregunta {index + 1}";
    }
}