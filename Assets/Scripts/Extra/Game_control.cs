using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_control : MonoBehaviour
{
    public static Game_control Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsCorrectAction(string action)
    {
        // Implement logic to check if the action is correct
        // This is where you check your sequence of actions
        return true; // Placeholder, implement actual check
    }

    public void ShowFeedback(string message)
    {
        // Implement logic to show feedback to the player
        Debug.Log(message); // For example, use Debug.Log for now
    }
}
