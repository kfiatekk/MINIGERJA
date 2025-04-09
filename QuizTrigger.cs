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

            if (quizManager.remainingAttempts <= 0)
            {
                Debug.Log("Nie masz ju� wi�cej pr�b dost�pnych. Nie mo�esz uruchomi� tej mini gry.");
                return;
            }

            if (quizManager.isQuizCompleted)
            {
                Debug.Log("Quiz zosta� ju� rozwi�zany z sukcesem. Nie mo�esz go ponownie uruchomi�.");
                return;
            }

            // Nowa blokada: Sprawdzanie, czy gra ju� trwa
            if (quizPanel.activeSelf)
            {
                Debug.Log("Gra ju� trwa. Nie mo�na jej ponownie uruchomi�.");
                return;
            }

            if (quizManager.canRetryQuiz)
            {
                Debug.Log("Rozpoczynasz quiz od nowa.");
                quizPanel.SetActive(true);
                gameManager.SetActive(true);
                quizManager.Start(); // Uruchomienie quizu
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