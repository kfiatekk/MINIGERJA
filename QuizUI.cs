using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public Text questionText;
    public Button optionAButton;
    public Button optionBButton;
    public Button optionCButton;
    public Text scoreText;

    public GameObject resultsPanel;
    public Text resultsText;

    public int score = 0;
    private bool quizEnded = false;
    private float resultsDisplayTime = 5f; // Czas wyœwietlania podsumowania wyników
    private float resultsTimer = 0f;

    private int remainingAttempts = 3; // Iloœæ dostêpnych prób quizu

    void Start()
    {
        optionAButton.onClick.AddListener(() => OnOptionSelected("A"));
        optionBButton.onClick.AddListener(() => OnOptionSelected("B"));
        optionCButton.onClick.AddListener(() => OnOptionSelected("C"));

        UpdateScoreText();
    }

    void Update()
    {
        if (resultsPanel.activeSelf)
        {
            resultsTimer += Time.deltaTime;

            if (resultsTimer >= resultsDisplayTime)
            {
                resultsPanel.SetActive(false);
                resultsTimer = 0f;

                if (quizEnded && remainingAttempts > 0)
                {
                    ResetQuiz();
                }
            }
        }
    }

    public void OnOptionSelected(string option)
    {
        if (quizEnded)
        {
            Debug.LogWarning("Klikniêto po zakoñczeniu quizu");
            return;
        }

        FindObjectOfType<QuizManager>().OnOptionSelected(option);
    }

    public void DisplayQuestion(Question question, int currentIndex, int total)
    {
        questionText.text = $"Pytanie {currentIndex}/{total}:\n{question.questionText}";
        optionAButton.GetComponentInChildren<Text>().text = question.options[0];
        optionBButton.GetComponentInChildren<Text>().text = question.options[1];
        optionCButton.GetComponentInChildren<Text>().text = question.options[2];

        ResetButtonColors();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Wynik: " + score;
    }

    public void IncorrectAnswer(string option)
    {
        Debug.Log("Z³a odpowiedŸ");
        Button selectedButton = null;
        switch (option)
        {
            case "A": selectedButton = optionAButton; break;
            case "B": selectedButton = optionBButton; break;
            case "C": selectedButton = optionCButton; break;
        }

        if (selectedButton != null)
        {
            ColorBlock cb = selectedButton.colors;
            cb.normalColor = Color.red;
            selectedButton.colors = cb;
        }
    }

    public void ResetButtonColors()
    {
        ColorBlock cb = optionAButton.colors;
        cb.normalColor = Color.white;
        optionAButton.colors = cb;

        cb = optionBButton.colors;
        cb.normalColor = Color.white;
        optionBButton.colors = cb;

        cb = optionCButton.colors;
        cb.normalColor = Color.white;
        optionCButton.colors = cb;
    }

    public void EndQuiz()
    {
        quizEnded = true;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        resultsPanel.SetActive(true);
        resultsTimer = 0f;

        if (remainingAttempts <= 0)
        {
            Debug.Log("Koniec quizu. Brak dostêpnych prób.");
            quizEnded = true;
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            resultsPanel.SetActive(true);
            resultsTimer = 0f;
        }
    }

    public void ShowFinalResults(int correct, int incorrect, bool isPassed)
    {
        if (resultsText != null)
        {
            string resultMessage;
            if (isPassed)
            {
                resultMessage = "Gratulacje! Zda³eœ quiz!";
            }
            else
            {
                remainingAttempts--;
                resultMessage = remainingAttempts > 0
                    ? $"Niestety, nie uda³o Ci siê zaliczyæ quizu. Pozosta³e próby: {remainingAttempts}"
                    : "Niestety, nie uda³o Ci siê zaliczyæ quizu. Brak dostêpnych prób.";
            }

            resultsText.text = $"{resultMessage}\nPoprawnych: {correct}\nB³êdnych: {incorrect}";
            resultsPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Brak przypisanego Text w resultsPanel!");
        }
    }

    private void ResetQuiz()
    {
        quizEnded = false;
        score = 0;
        UpdateScoreText();
        FindObjectOfType<QuizManager>().Start(); // Resetuje pytania w QuizManager
        gameObject.SetActive(true);
    }
}