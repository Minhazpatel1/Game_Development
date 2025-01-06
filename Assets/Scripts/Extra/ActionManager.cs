using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private int currentStep = 0;
    public GameObject errorPanel; // Assign in Unity Editor
    void Start()
    {
        errorPanel.SetActive(false); // Hide the panel initially
    }
    public static ActionManager Instance { get; private set; }

    private List<string> instructions = new List<string>()
    {
        "MAR<-PC",
        "IR<-M[MAR]",
        "PC<-PC+1",
        "MAR<-IR[11-0]",
        "MBR<-M[MAR]",
        "AC<-MBR"
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ExecuteAction(string action)
    {
        if (instructions[currentStep] != action)
        {
            ShowError();
        }
        else
        {
            currentStep++;
            if (currentStep >= instructions.Count)
            {
                // Optionally reset or handle the completion of instructions
                currentStep = 0; // Reset for simplicity
            }
        }
    }

    public void ShowError()
    {
        errorPanel.SetActive(true);
    }

    public void ResetSteps()
    {
        currentStep = 0;
    }

    public void OnCancelButton()
    {
        errorPanel.SetActive(false);
    }
}
