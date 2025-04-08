using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    public GameObject quizPanel;
    public GameObject gameManager;
    private bool isPlayerNear = false;

    private bool quizAlreadyCompleted = false; // Flaga sprawdzaj�ca, czy quiz zosta� uko�czony

    void Start()
    {
        gameManager.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (quizAlreadyCompleted)
            {
                Debug.Log("Quiz zosta� ju� rozwi�zany. Nie mo�esz podej�� ponownie.");
                return; // Nie pozwalaj na ponowne uruchomienie quizu
            }

            QuizManager quizManager = gameManager.GetComponent<QuizManager>();

            if (quizManager.isQuizCompleted)
            {
                quizAlreadyCompleted = true; // Oznacz quiz jako uko�czony
                Debug.Log("Quiz zosta� ju� rozwi�zany. Nie mo�esz podej�� ponownie.");
                return;
            }

            quizPanel.SetActive(true);
            gameManager.SetActive(true);
            quizManager.Start(); // Uruchom quiz
            Time.timeScale = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isPlayerNear = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isPlayerNear = false;
    }
}