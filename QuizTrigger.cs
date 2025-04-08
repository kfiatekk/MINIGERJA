using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    public GameObject quizPanel;
    public GameObject gameManager;
    private bool isPlayerNear = false;

    void Start()
    {
        gameManager.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            QuizManager quizManager = gameManager.GetComponent<QuizManager>();

            if (quizManager.isQuizCompleted)
            {
                Debug.Log("Quiz zosta³ ju¿ rozwi¹zany z sukcesem. Nie mo¿esz go ponownie uruchomiæ.");
                return;
            }

            if (quizManager.canRetryQuiz)
            {
                Debug.Log("Rozpoczynasz quiz od nowa.");
                quizPanel.SetActive(true);
                gameManager.SetActive(true);
                quizManager.Start(); // Uruchom quiz
                Time.timeScale = 0f;
            }
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