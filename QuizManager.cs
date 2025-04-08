using UnityEngine;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public QuizUI quizUI;

    private List<Question> questions;
    private int currentQuestionIndex = 0;
    private int incorrectAnswers = 0;
    private const int minimumCorrectAnswersToPass = 5;

    public bool isQuizCompleted = false;
    public bool canRetryQuiz = true;

    public void Start()
    {
        if (isQuizCompleted)
        {
            Debug.Log("Quiz zostal juz ukonczony z sukcesem. Nie mozesz go ponownie rozwiazac.");
            return;
        }

        if (!canRetryQuiz)
        {
            Debug.Log("Ostatni wynik byl ponizej progu. Mozesz sprobowac ponownie.");
        }

        questions = new List<Question>
{
    new Question
    {
        questionText = "Czym jest zmienna?",
        options = new List<string> { "Petla", "Pamiec na dane", "Funkcja" },
        correctOption = "B"
    },
    new Question
    {
        questionText = "Symbol porownania wartosci:",
        options = new List<string> { "=", "==", "=>" },
        correctOption = "B"
    },
    new Question
    {
        questionText = "Do czego sluzy petla?",
        options = new List<string> { "Do warunku", "Do liczenia", "Do powtorzen" },
        correctOption = "C"
    },
    new Question
    {
        questionText = "Jak obslugiwac wyjatki?",
        options = new List<string> { "if", "catch", "loop" },
        correctOption = "B"
    },
    new Question
    {
        questionText = "return w funkcji:",
        options = new List<string> { "Zwraca wartosc", "Tworzy zmienna", "Usuwa funkcje" },
        correctOption = "A"
    },
    new Question
    {
        questionText = "do-while robi co najmniej:",
        options = new List<string> { "Jeden raz", "Zero razy", "Dwa razy" },
        correctOption = "A"
    },
    new Question
    {
        questionText = "null oznacza:",
        options = new List<string> { "Brak wartosci", "Zero", "Prawda" },
        correctOption = "A"
    },
    new Question
    {
        questionText = "Czym jest funkcja?",
        options = new List<string> { "Zmienna", "Blok kodu", "Petla" },
        correctOption = "B"
    },
    new Question
    {
        questionText = "Co robi instrukcja if?",
        options = new List<string> { "Powtarza", "Warunkuje", "Tworzy" },
        correctOption = "B"
    },
    new Question
    {
        questionText = "Jaki typ dla liczb calkowitych?",
        options = new List<string> { "int", "float", "string" },
        correctOption = "A"
    },
    new Question
    {
        questionText = "float przechowuje:",
        options = new List<string> { "Tekst", "Liczby calkowite", "Liczby zmiennoprzecinkowe" },
        correctOption = "C"
    },
    new Question
    {
        questionText = "bool moze byc:",
        options = new List<string> { "int lub float", "true lub false", "tekst" },
        correctOption = "B"
    },
    new Question
    {
        questionText = "Tablica to:",
        options = new List<string> { "Zmienna", "Zbior elementow", "Funkcja" },
        correctOption = "B"
    },
    new Question
    {
        questionText = "new w C# oznacza:",
        options = new List<string> { "Nowy typ", "Nowy obiekt", "Nowy warunek" },
        correctOption = "B"
    },
    new Question
    {
        questionText = "Ktory z ponizszych to petla?",
        options = new List<string> { "if", "loop", "for" },
        correctOption = "C"
    }
};


        currentQuestionIndex = 0;
        incorrectAnswers = 0;
        quizUI.score = 0;

        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            var q = questions[currentQuestionIndex];
            quizUI.DisplayQuestion(q, currentQuestionIndex + 1, questions.Count);
        }
        else
        {
            bool isPassed = quizUI.score >= minimumCorrectAnswersToPass;

            if (isPassed)
            {
                isQuizCompleted = true;
                canRetryQuiz = false;
            }
            else
            {
                Debug.Log("Nie zaliczyles quizu. Mozesz sprobowac ponownie.");
                canRetryQuiz = true;
            }

            quizUI.ShowFinalResults(quizUI.score, incorrectAnswers, isPassed);
            quizUI.EndQuiz();
        }
    }

    public void OnOptionSelected(string option)
    {
        if (currentQuestionIndex >= questions.Count)
        {
            Debug.LogWarning("Kliknieto po zakonczeniu quizu");
            return;
        }

        if (option == questions[currentQuestionIndex].correctOption)
        {
            quizUI.score++;
            quizUI.UpdateScoreText();
        }
        else
        {
            incorrectAnswers++;
            quizUI.IncorrectAnswer(option);
        }

        currentQuestionIndex++;
        DisplayQuestion();
    }
}

[System.Serializable]
public class Question
{
    public string questionText;
    public List<string> options;
    public string correctOption;
}
