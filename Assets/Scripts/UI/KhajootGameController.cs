using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla la escena del juego Kahoot
/// </summary>
public class KhajotGameController : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public TMP_Text questionNumberText;
    public Button[] answerButtons;
    public Button nextButton;
    public GameObject resultPanel;
    public TMP_Text resultText;
    public TMP_Text correctAnswerText;

    private Khajot currentKahoot;
    private int currentQuestionIndex = 0;
    private int currentScore = 0;
    private float timeRemaining;
    private bool questionAnswered = false;
    private int selectedAnswerIndex = -1;

    /// <summary>
    /// Inicializa la escena del juego
    /// </summary>
    void Start()
    {
        // Obtener el Kahoot seleccionado
        string selectedKahootName = SceneController.GetSelectedKahoot();
        currentKahoot = GetKahootByName(selectedKahootName);

        if (currentKahoot == null)
        {
            Debug.LogError("No se pudo cargar el Kahoot seleccionado");
            SceneController.LoadKahootSelector();
            return;
        }

        // Configurar botones
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].onClick.AddListener(() => OnAnswerClicked(index));
        }

        nextButton.onClick.AddListener(OnNextClicked);

        resultPanel.SetActive(false);
        nextButton.gameObject.SetActive(false);

        // Iniciar primera pregunta
        LoadQuestion(currentQuestionIndex);
    }

    /// <summary>
    /// Obtiene un Kahoot por su nombre
    /// </summary>
    private Khajot GetKahootByName(string name)
    {
        List<Khajot> allKahoots = GameManager.Instance.GetAvailableKahoots();
        foreach (Khajot kahoot in allKahoots)
        {
            if (kahoot.khajotName == name)
            {
                return kahoot;
            }
        }
        return null;
    }

    /// <summary>
    /// Carga una pregunta específica
    /// </summary>
    private void LoadQuestion(int questionIndex)
    {
        if (currentKahoot == null || questionIndex >= currentKahoot.questions.Count)
        {
            EndGame();
            return;
        }

        Question question = currentKahoot.questions[questionIndex];

        // Actualizar UI
        questionText.text = question.question;
        questionNumberText.text = $"{questionIndex + 1}";
        scoreText.text = $"Puntuación: {currentScore}";

        // Configurar respuestas
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < question.answers.Count)
            {
                answerButtons[i].GetComponentInChildren<TMP_Text>().text = question.answers[i];
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].interactable = true;
                answerButtons[i].image.color = Color.white;
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }

        // Iniciar temporizador
        timeRemaining = question.time;
        questionAnswered = false;
        selectedAnswerIndex = -1;

        StartCoroutine(QuestionTimer());
    }

    /// <summary>
    /// Temporizador para la pregunta actual
    /// </summary>
    private IEnumerator QuestionTimer()
    {
        while (timeRemaining > 0 && !questionAnswered)
        {
            timerText.text = $"Tiempo: {timeRemaining:F1}s";
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        if (!questionAnswered)
        {
            // Tiempo agotado
            OnTimeOut();
        }
    }

    /// <summary>
    /// Cuando se hace clic en una respuesta
    /// </summary>
    private void OnAnswerClicked(int answerIndex)
    {
        if (questionAnswered) return;

        questionAnswered = true;
        selectedAnswerIndex = answerIndex;

        Question currentQuestion = currentKahoot.questions[currentQuestionIndex];
        bool isCorrect = (answerIndex == currentQuestion.rightAnswerIndex);

        // Mostrar resultado
        resultPanel.SetActive(true);
        resultText.text = isCorrect ? "¡CORRECTO!" : "INCORRECTO";
        resultText.color = isCorrect ? Color.green : Color.red;

        // Mostrar respuesta correcta si fue incorrecta
        if (!isCorrect)
        {
            correctAnswerText.text = $"Respuesta correcta: {currentQuestion.answers[currentQuestion.rightAnswerIndex]}";
            correctAnswerText.gameObject.SetActive(true);
        }
        else
        {
            correctAnswerText.gameObject.SetActive(false);
        }

        // Calcular puntos
        int pointsEarned = isCorrect ? Mathf.Max(100, Mathf.RoundToInt(timeRemaining * 10)) : 0;
        currentScore += pointsEarned;
        scoreText.text = $"Puntuación: {currentScore}";

        // Resaltar respuesta correcta
        answerButtons[currentQuestion.rightAnswerIndex].image.color = Color.green;

        // Mostrar botón siguiente
        nextButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Cuando se agota el tiempo
    /// </summary>
    private void OnTimeOut()
    {
        questionAnswered = true;
        resultPanel.SetActive(true);
        resultText.text = "TIEMPO AGOTADO";
        resultText.color = Color.yellow;

        Question currentQuestion = currentKahoot.questions[currentQuestionIndex];
        correctAnswerText.text = $"Respuesta correcta: {currentQuestion.answers[currentQuestion.rightAnswerIndex]}";
        correctAnswerText.gameObject.SetActive(true);

        answerButtons[currentQuestion.rightAnswerIndex].image.color = Color.green;
        nextButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Cuando se hace clic en Siguiente
    /// </summary>
    private void OnNextClicked()
    {
        currentQuestionIndex++;
        resultPanel.SetActive(false);
        nextButton.gameObject.SetActive(false);
        LoadQuestion(currentQuestionIndex);
    }

    /// <summary>
    /// Cuando se hace clic en Salir
    /// </summary>
    private void OnQuitClicked()
    {
        SceneController.LoadKahootSelector();
    }

    /// <summary>
    /// Finaliza el juego
    /// </summary>
    private void EndGame()
    {
        // Guardar puntuación final
        GameManager.Instance.EndGame(currentScore);
    }
}