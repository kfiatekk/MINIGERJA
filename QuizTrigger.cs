using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    public GameObject quizPanel;
    public GameObject gameManager; // Referencja do GameManager
    private bool isPlayerNear = false;

    void Start()
    {
        gameManager.SetActive(false); // Wy³¹cz GameManager na pocz¹tku
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Show the quiz panel
            quizPanel.SetActive(true);
            // Activate the GameManager
            gameManager.SetActive(true);
            // Optionally, pause the game
            Time.timeScale = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}