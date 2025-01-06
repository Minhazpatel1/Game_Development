using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastScene : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText; // Assign in Unity Editor

    void Start()
    {
        // Retrieve the final score from Game_Manager
        int finalScore = 0;

        // Display the final score
        if (finalScoreText != null)
        {
            finalScore = Game_Manager.Instance.OverallScore();
            finalScoreText.text = "Final Score: " + finalScore;
        }
        else
        {
            Debug.LogWarning("Final score text not assigned!");
        }
    }

    public void OnRestartButton()
    {
        // Reset the score in Game_Manager and load the first scene
        Game_Manager.Instance.AddScore(-Game_Manager.Instance.OverallScore()); // Reset score to 0
        SceneManager.LoadScene(1); // Load the menu scene
    }

    public void OnExitButton()
    {
        // Exit the application
        Application.Quit();
    }
}
