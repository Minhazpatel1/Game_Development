using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro; // Add this line


public class PC_behaviour : MonoBehaviour
{
    public static int value = 100;
    private TextMeshPro textMesh;
    public GameObject Panel;
    private Player_behaviour playerScript;
    public static bool incrementActionDone = false;

    void Start()
    {
        if (Game_Manager.Instance != null)
        {
            value = Game_Manager.Instance.InitialPCValue; // Retrieve value from Game_Manager
            Debug.Log("PC Value initialized from Game_Manager: " + value);
        }
        textMesh = GetComponent<TextMeshPro>();
        UpdateTextMesh();
        Panel.SetActive(false);
    }

    public void setpcvalue(int val)
    {
        value = val;
        Game_Manager.Instance.InitialPCValue = value; // Save the updated value to Game_Manager
        Debug.Log("PC Value updated in Game_Manager: " + value);
        UpdateTextMesh();
    }

    public void OnIncrementButton()
    {
        if(Game_Manager.Instance.expectedAction == "PC<-PC+1")
        {
            value++;
            incrementActionDone = true;
            setpcvalue(value);
            UpdateTextMesh();
            Game_Manager.Instance.AddScore(100);
            Debug.Log("PC Value incremented and saved to Game_Manager: " + value);
            LoadNextScene();
        }
        else
        {
            Game_Manager.Instance.ShowError1();
        }
        Panel.SetActive(false);
    }

    void UpdateTextMesh()
    {
        if (textMesh != null)
        {
            int totalWidth = 6;
            string valueText = value.ToString("D3");
            string text = valueText;
            textMesh.text = "PC: \t" + text.PadLeft(totalWidth);
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
            Panel.SetActive(true);
            playerScript = other.gameObject.GetComponent<Player_behaviour>();
        }
    }

    public void OnCollectButton()
    {
        if (playerScript != null & Game_Manager.Instance.expectedAction == "MAR<-PC")
        {
            playerScript.CollectValue(value);
            UpdateTextMesh();
        }
        else
        {
            Game_Manager.Instance.ShowError1();
        }
        Panel.SetActive(false);
    }

    public void OnTransferButton()
    {
        if (playerScript != null)
        {
            if (Game_Manager.Instance.expectedAction == "")
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