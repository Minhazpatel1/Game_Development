using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For displaying the score

public class Practice : MonoBehaviour
{
    public static Game_Manager Instance { get; private set; }
    public GameObject Panel;
    private Queue<string> expectedActions = new Queue<string>();
    public TextMeshProUGUI scoreText; // Assign in Unity Editor
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            //Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize expected actions
        expectedActions.Enqueue("MAR<-PC");
        // Add other actions as needed
    }

    void Start()
    {
        Panel.SetActive(false); // Hide the panel initially
        UpdateScoreDisplay();
    }

    public void ShowError()
    {
        Panel.SetActive(true);
    }

    public void OnCancelButton()
    {
        Panel.SetActive(false);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void CheckAction(string playerAction)
    {
        if (expectedActions.Count == 0)
        {
            Debug.LogError("No expected actions remaining.");
            return;
        }

        string expectedAction = expectedActions.Peek(); // Look at the next expected action without removing it
        if (playerAction == expectedAction)
        {
            // Correct action, remove it from the queue
            expectedActions.Dequeue();
            AddScore(100); // Add 100 points; adjust value as needed
        }
        else
        {
            // Incorrect action, show error
            ShowError();
        }
    }
}
