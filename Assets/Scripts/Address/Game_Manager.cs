using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance { get; private set; }
    public int InitialPCValue { get; set; }
    public GameObject Panel;
    public GameObject Panel1;
    public string expectedAction = "MAR<-PC";
    public TextMeshProUGUI scoreText;
    private static int randomAddress = -1;
    private static int score = 0;
    public demo address1;
    public demo address2;
    public demo address3;
    public demo address4;
    public demo address5;

    void Start()
    {
        Panel.SetActive(false);
        Panel1.SetActive(false);
        UpdateScoreDisplay();
        if (address1 != null)
        {
            address1.UpdateDisplay();
        }
        AssignLastThreeDigitsOfAddress1();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AssignLastThreeDigitsOfAddress1()
    {
        if (randomAddress == -1)
        {
            randomAddress = UnityEngine.Random.Range(2, 6);
        }

        int lastThreeDigits = address1 != null ? address1.value2 % 1000 : 0;

        demo selectedAddress = null;

        switch (randomAddress)
        {
            case 2:
                selectedAddress = address2;
                break;
            case 3:
                selectedAddress = address3;
                break;
            case 4:
                selectedAddress = address4;
                break;
            case 5:
                selectedAddress = address5;
                break;
        }

        if (selectedAddress != null)
        {
            selectedAddress.value1 = lastThreeDigits;
            selectedAddress.UpdateDisplay();
        }
        //else
        //{
        //    Debug.LogError($"Selected Address_{randomAddress} is not assigned.");
        //}
    }

    public void ShowError()
    {
        AddScore(-20);
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
