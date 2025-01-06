using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this line

public class Address_3 : MonoBehaviour
{
    public int value1;
    public int value2;
    [SerializeField]
    private TextMeshPro textMesh;
    public GameObject Panel; // Assign in Unity Editor
    private Player_behaviour playerScript;

    public static int addr1 = -1; // Use -1 as a flag to check if values are set
    public static int addr2 = -1;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        // Only set random values if addr1 and addr2 haven't been set yet
        if (addr1 == -1 && addr2 == -1)
        {
            set_add_val(); // Set random values only once
        }

        // Assign the static values to the instance variables
        value1 = addr1;
        value2 = addr2;
        Panel.SetActive(false); // Hide the panel initially
        UpdateDisplay();
    }

    public void set_add_val()
    {
        addr1 = Random.Range(100, 1000); // Random value between 100 and 999
        addr2 = Random.Range(1000, 10000);
    }

    public void UpdateDisplay()
    {
        string formattedValue2 = value2.ToString("D4");
        textMesh.text = $"{value1}:   {formattedValue2}";
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
        // Simplified logic: Just pass value2 to the player
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
        // Logic to store the player's value into this address
        if (playerScript != null && Game_Manager.Instance.expectedAction == "")
        {
            value2 = playerScript.playerValue;
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
