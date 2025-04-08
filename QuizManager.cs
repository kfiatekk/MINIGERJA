using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; // Add this line to include the UnityEngine.UI namespace

public class QuizManager : MonoBehaviour
{
    public QuizUI quizUI;

    private List<Question> questions;
    private int currentQuestionIndex = 0;

    void Start()
    {
        questions = new List<Question>
        {
            new Question
            {
                questionText = "Która linijka poprawnie wypisuje tekst 'Hello World' w Javie?",
                options = new List<string>
                {
                    "System.out.printline(\"Hello World\");",
                    "System.out.println(\"Hello World\");",
                    "print(\"Hello World\");"
                },
                correctOption = "B"
            },
            new Question
            {
                questionText = "Jakie s³owo kluczowe w Javie s³u¿y do tworzenia nowego obiektu?",
                options = new List<string>
                {
                    "new",
                    "create",
                    "object"
                },
                correctOption = "A"
            },
            new Question
            {
                questionText = "Jak poprawnie zdefiniowaæ klasê w Javie?",
                options = new List<string>
                {
                    "function MojaKlasa {}",
                    "class MojaKlasa {}",
                    "define class MojaKlasa {}"
                },
                correctOption = "B"
            },
            new Question
            {
                questionText = "Który typ danych s³u¿y do przechowywania liczb ca³kowitych w Javie?",
                options = new List<string>
                {
                    "int",
                    "string",
                    "float"
                },
                correctOption = "A"
            },
            new Question
            {
                questionText = "Co oznacza skrót JVM?",
                options = new List<string>
                {
                    "Java Variable Manager",
                    "Java Virtual Machine",
                    "Java Visual Mode"
                },
                correctOption = "B"
            },
            // Add more questions here
        };

        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            quizUI.questionText.text = questions[currentQuestionIndex].questionText;
            quizUI.optionAButton.GetComponentInChildren<Text>().text = questions[currentQuestionIndex].options[0];
            quizUI.optionBButton.GetComponentInChildren<Text>().text = questions[currentQuestionIndex].options[1];
            quizUI.optionCButton.GetComponentInChildren<Text>().text = questions[currentQuestionIndex].options[2];

            // Reset timer for each question
            quizUI.ResetTimer();
        }
        else
        {
            quizUI.EndQuiz();
        }
    }

    public void OnOptionSelected(string option)
    {
        if (currentQuestionIndex < questions.Count)
        {
            if (option == questions[currentQuestionIndex].correctOption)
            {
                quizUI.score++;
                quizUI.scoreText.text = "Score: " + quizUI.score;
            }
            else
            {
                quizUI.IncorrectAnswer();
            }

            currentQuestionIndex++;
            DisplayQuestion();
        }
        else
        {
            Debug.LogWarning("Index out of range: " + currentQuestionIndex);
        }
    }
}

[System.Serializable]
public class Question
{
    public string questionText;
    public List<string> options;
    public string correctOption;
}