using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Include this for UI Text
using TMPro; // Add this line

public class AC_behaviour : MonoBehaviour
{
    public static int value = 0;
    private TextMeshPro textMesh;
    public GameObject Panel;
    private Player_behaviour playerScript;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        UpdateTextMesh();
        Panel.SetActive(false); // Hide the panel initially
    }

    void UpdateTextMesh()
    {
        if (textMesh != null)
        {
            int totalWidth = 5;
            string valueText = value.ToString("D4");
            string text = valueText;
            textMesh.text = "AC:\t" + text.PadLeft(totalWidth);
        }
        else
        {
            Debug.LogWarning("TextMesh component not found on the game object!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Panel.SetActive(true); // Show the panel
            playerScript = other.gameObject.GetComponent<Player_behaviour>();
        }
    }

    public void OnCollectButton()
    {
        if (playerScript != null)
        {
            if (Game_Manager.Instance.expectedAction == "")
            {
                playerScript.CollectValue(value);
                UpdateTextMesh();
            }
            else
            {
                Game_Manager.Instance.ShowError1();
            }
        }
        Panel.SetActive(false);
    }

    public void OnTransferButton()
    {
        if (playerScript != null)
        {
            if(Game_Manager.Instance.expectedAction == "AC<-MBR")
            {
                value = playerScript.playerValue;
                playerScript.TransferValue(playerScript.playerValue);
                UpdateTextMesh();
                Game_Manager.Instance.AddScore(100);
                LoadNextScene();
            }
            else
            {
                Game_Manager.Instance.ShowError1();
            }
        }
        Panel.SetActive(false);
    }

    public void OnCancelButton()
    {
        Panel.SetActive(false);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

