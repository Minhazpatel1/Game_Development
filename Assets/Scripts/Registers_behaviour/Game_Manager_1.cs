using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_Manager_1 : MonoBehaviour
{
    public static Game_Manager_1 Instance { get; private set; }
    public int InitialPCValue { get; set; }
    public GameObject Panel;
    public GameObject Panel1;
    public string expectedAction = "MAR<-PC";
    public TextMeshProUGUI scoreText; // Assign in Unity Editor
    private static int randomAddress = -1;
    private static int score = 0;
    public Address_1 address1;
    public Address_2 address2;
    public Address_3 address3;
    public Address_4 address4;
    public Address_5 address5;

    void Start()
    {
        Panel.SetActive(false); // Hide the panel initially
        Panel1.SetActive(false); // Hide the panel initially
        UpdateScoreDisplay(); // Initialize score display
        // Ensure Address_1 is initialized before assigning values
        if (address1 != null)
        {
            address1.Update(); // Make sure Address_1 display is updated
        }
        AssignLastThreeDigitsOfAddress1();

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist Game_Manager across scenes
            Debug.Log("Game_Manager Awake: PCValue = " + InitialPCValue);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate Game_Manager instances
        }
    }

    public void AssignLastThreeDigitsOfAddress1()
    {
        if (randomAddress == -1)
        {
            randomAddress = Random.Range(2, 4); // Generate random number between 2 and 5
        }
        
        int lastThreeDigits = address1 != null ? address1.value2 % 1000 : 0; // Last 3 digits of value2


        // Assign the last three digits to the randomly chosen address, only if the address is not null
        switch (randomAddress)
        {
            case 2:
                if (address2 != null)
                {
                    address2.value1 = lastThreeDigits;
                    address2.UpdateDisplay(); // Call a method to update the display for Address_2
                }
                else
                {
                    Debug.LogError("Address_2 is not assigned.");
                }
                break;
            case 3:
                if (address3 != null)
                {
                    address3.value1 = lastThreeDigits;
                    address3.UpdateDisplay(); // Call a method to update the display for Address_3
                }
                else
                {
                    Debug.LogError("Address_3 is not assigned.");
                }
                break;
            case 4:
                if (address4 != null)
                {
                    address4.value1 = lastThreeDigits;
                    address4.UpdateDisplay(); // Call a method to update the display for Address_4
                }
                else
                {
                    Debug.LogError("Address_4 is not assigned.");
                }
                break;
            case 5:
                if (address5 != null)
                {
                    address5.value1 = lastThreeDigits;
                    address5.UpdateDisplay(); // Call a method to update the display for Address_5
                }
                else
                {
                    Debug.LogError("Address_5 is not assigned.");
                }
                break;
        }
    }

    public void ShowError()
    {
        AddScore(-20); // Deduct points for incorrect action
        Panel.SetActive(true);
    }
    public void ShowError1()
    {
        AddScore(-20); // Deduct points for incorrect action
        Panel1.SetActive(true);
    }
    public void OnCancelButton()
    {
        Panel.SetActive(false);
        Panel1.SetActive(false);
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
        if (playerAction == expectedAction)
            AddScore(100); // Add 100 points; adjust value as needed
        else
        {
            ShowError();
        }  
    }
    public int OverallScore()
    {
        return score;
    }
}

