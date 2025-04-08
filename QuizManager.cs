using UnityEngine;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public QuizUI quizUI;

    private List<Question> questions;
    private int currentQuestionIndex = 0;
    private int incorrectAnswers = 0;
    private const int minimumCorrectAnswersToPass = 5;

    public bool isQuizCompleted = false; // Czy quiz zosta� uko�czony?

    public void Start() // Zmieniono na public, aby umo�liwi� reset quizu
    {
        if (isQuizCompleted)
        {
            Debug.Log("Quiz zosta� ju� uko�czony. Nie mo�esz go ponownie rozwi�za�.");
            return;
        }

        questions = new List<Question>
        {
            new Question
            {
                questionText = "Co to jest zmienna w programowaniu?",
                options = new List<string>
                {
                    "Funkcja matematyczna",
                    "Miejsce do przechowywania danych",
                    "Instrukcja warunkowa"
                },
                correctOption = "B"
            },
            new Question
            {
                questionText = "Kt�ry symbol s�u�y do por�wnania warto�ci?",
                options = new List<string>
                {
                    "=",
                    "==",
                    "!="
                },
                correctOption = "B"
            },
            new Question
            {
                questionText = "Jak nazywa si� struktura, kt�ra wykonuje kod wielokrotnie?",
                options = new List<string>
                {
                    "Warunek",
                    "P�tla",
                    "Zmienne"
                },
                correctOption = "B"
            },
            new Question
            {
                questionText = "Do obs�ugi wyj�tk�w u�yj:",
                options = new List<string>
                {
                    "try",
                    "catch",
                    "throw"
                },
                correctOption = "B"
            },
            new Question
            {
                questionText = "Co robi return?",
                options = new List<string>
                {
                    "Stop",
                    "Zwraca",
                    "Deklaruje"
                },
                correctOption = "B"
            },
            new Question
            {
                questionText = "do-while vs while?",
                options = new List<string>
                {
                    "while raz",
                    "do-while raz",
                    "bez r�nicy"
                },
                correctOption = "B"
            },
            new Question
            {
                questionText = "null oznacza:",
                options = new List<string>
                {
                    "Brak warto�ci",
                    "Zero",
                    "Typ"
                },
                correctOption = "A"
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
            if (isPassed || !isPassed)
            {
                isQuizCompleted = true; // Oznacz quiz jako zako�czony
            }

            quizUI.ShowFinalResults(quizUI.score, incorrectAnswers, isPassed);
            quizUI.EndQuiz();
        }
    }

    public void OnOptionSelected(string option)
    {
        if (currentQuestionIndex >= questions.Count)
        {
            Debug.LogWarning("Klikni�to po zako�czeniu quizu");
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