using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public Text questionText;
    public Button optionAButton;
    public Button optionBButton;
    public Button optionCButton;
    public Text scoreText;
    public Text timerText;

    public int score = 0;
    private float timeRemaining = 30f; // Example time limit
    private bool isTimerRunning = false;

    void Start()
    {
        // Example question and options
        questionText.text = "What is the correct version of this Java code?";
        optionAButton.GetComponentInChildren<Text>().text = "Option A";
        optionBButton.GetComponentInChildren<Text>().text = "Option B";
        optionCButton.GetComponentInChildren<Text>().text = "Option C";

        optionAButton.onClick.AddListener(() => OnOptionSelected("A"));
        optionBButton.onClick.AddListener(() => OnOptionSelected("B"));
        optionCButton.onClick.AddListener(() => OnOptionSelected("C"));

        // Start the timer
        StartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
            }
            else
            {
                // Time's up, handle accordingly
                EndQuiz();
            }
        }
    }

    public void OnOptionSelected(string option)
    {
        // Example correct answer check
        FindObjectOfType<QuizManager>().OnOptionSelected(option);
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void ResetTimer()
    {
        timeRemaining = 30f;
        timerText.text = "Time: " + timeRemaining.ToString();
        StartTimer();
    }

    public void IncorrectAnswer()
    {
        Debug.Log("Incorrect answer selected");
        // Change color of the button to indicate incorrect answer
        ColorBlock cb = optionAButton.colors;
        cb.normalColor = Color.red;
        optionAButton.colors = cb;
    }

    public void EndQuiz()
    {
        isTimerRunning = false;
        // Hide quiz panel and show results
        gameObject.SetActive(false);
        Time.timeScale = 1f; // Resume game if paused
    }
}