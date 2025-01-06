using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Diagnostics;

public class demo : MonoBehaviour
{
    public int value1;
    public int value2;
    [SerializeField]
    private TextMeshPro textMesh;
    public GameObject Panel; // Assign in Unity Editor
    private Player_behaviour playerScript;

    public static Dictionary<int, (int value1, int value2)> addressValues = new Dictionary<int, (int value1, int value2)>(); // Shared static values
    public int addressID; // Unique ID for each address (1 for Address_1, 2 for Address_2, etc.)
    public bool isSpecialAddress; // Set true for Address_1 or special addresses
    public PC_behaviour pcBehaviour; // Only used by Address_1 for PC

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        Panel.SetActive(false); // Hide the panel initially

        // If values haven't been set for this addressID, set them
        if (!addressValues.ContainsKey(addressID))
        {
            int randValue1 = UnityEngine.Random.Range(100, 1000); // Random value between 100 and 999
            int randValue2 = UnityEngine.Random.Range(1000, 10000); // Random value between 1000 and 9999
            addressValues[addressID] = (randValue1, randValue2);
        }

        // Assign values from the dictionary
        value1 = addressValues[addressID].value1;
        value2 = addressValues[addressID].value2;
    }

    void Start()
    {
        if (isSpecialAddress)
        {
            if (pcBehaviour != null)
            {
                pcBehaviour.setpcvalue(value1); // Assign PC value for Address_1
            }
            else
            {
                UnityEngine.Debug.LogWarning("PC_behaviour is not assigned for the special address.");
            }
        }
        UpdateDisplay(); // Update the text on the UI
    }

    // Update the text display for the address
    public void UpdateDisplay()
    {
        string formattedValue2 = value2.ToString("D4");
        string formattedValue1 = isSpecialAddress ? $"{value1:D3}" : $"{value1:D3}"; // Different formatting for Address_1
        textMesh.text = $"{formattedValue1}:   {formattedValue2}";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScript = other.GetComponent<Player_behaviour>();
            if (playerScript != null)
            {
                Panel.SetActive(true); // Show the panel only if playerScript is found
            }
        }
    }

    public void OnCollectButton()
    {
        if (playerScript != null)
        {
            if (Game_Manager.Instance.expectedAction == "MBR<-M[MAR]" ||
                Game_Manager.Instance.expectedAction == "IR<-M[MAR]")
            {
                playerScript.CollectValue(value2);
            }
            else
            {
                Game_Manager.Instance.ShowError1();
            }
        }
        Panel.SetActive(false);
    }

    public void OnStoreButton()
    {
        if (playerScript != null && Game_Manager.Instance.expectedAction == "")
        {
            value2 = playerScript.playerValue;
            addressValues[addressID] = (value1, value2); // Update the dictionary
            UpdateDisplay(); // Update the text mesh to show the new value
        }
        else
        {
            Game_Manager.Instance.ShowError1();
        }

        Panel.SetActive(false);
    }

    public void OnCancelButton()
    {
        Panel.SetActive(false);
    }
}
