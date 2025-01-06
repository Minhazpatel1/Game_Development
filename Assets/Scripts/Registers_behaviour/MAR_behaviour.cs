using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Include this for UI Text
using TMPro; // Add this line

public class MAR_behaviour : MonoBehaviour
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
            int totalWidth = 6;
            string valueText = value.ToString("D3");
            string text = valueText;
            textMesh.text = "MAR: \t" + text.PadLeft(totalWidth);
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
            if(Game_Manager.Instance.expectedAction == "")
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
        //if (playerScript != null)
        //{
        //int lastThreeDigitsOfIR = IR_behaviour.value % 1000;
        //Debug.Log("lastThreeDigitOfIR");
        /* if (Game_Manager.Instance.expectedAction == "MAR<-PC" && playerScript.playerValue != PC_behaviour.value)
        {
        // The player performed the wrong action
            Game_Manager.Instance.ShowError();
            Panel.SetActive(false);
            return; // Exit the method without changing the scene
        }

        if (Game_Manager.Instance.expectedAction == "MAR<-IR[11-0]" && playerScript.playerValue != lastThreeDigitsOfIR)
        {
            Game_Manager.Instance.ShowError();
            Panel.SetActive(false);
            return; // Exit the method without changing the scene
        } */

        /* if (playerScript.playerValue == PC_behaviour.value)
        {
            Game_Manager.Instance.CheckAction("MAR<-PC");
        }
        else if (playerScript.playerValue == lastThreeDigitsOfIR)
        {
            Game_Manager.Instance.CheckAction("MAR<-IR[11-0]");
        }
        else
        {
            Game_Manager.Instance.ShowError();
            Panel.SetActive(false);
            return;
        }
        // Call before changing the value to check if this action is expected now
        value = playerScript.playerValue;
        playerScript.TransferValue(playerScript.playerValue);
        UpdateTextMesh();
        // Load the next scene
        LoadNextScene();
    }
    Panel.SetActive(false); */

        if (playerScript != null)
        {
            if(Game_Manager.Instance.expectedAction == "MAR<-PC" ||
               Game_Manager.Instance.expectedAction == "MAR<-IR[11-0]")
            {
                int lastThreeDigitsOfIR = IR_behaviour.value % 1000;

                bool actionCorrect = false; // Flag to check if the action is correct

                if (playerScript.playerValue == PC_behaviour.value)
                {
                    Game_Manager.Instance.CheckAction("MAR<-PC");
                    actionCorrect = Game_Manager.Instance.expectedAction == "MAR<-PC";
                }
                else if (playerScript.playerValue == lastThreeDigitsOfIR)
                {
                    Game_Manager.Instance.CheckAction("MAR<-IR[11-0]");
                    actionCorrect = Game_Manager.Instance.expectedAction == "MAR<-IR[11-0]";
                }
                else
                {
                    Game_Manager.Instance.ShowError();
                    Panel.SetActive(false);
                    return;
                }

                if (actionCorrect)
                {
                    value = playerScript.playerValue;
                    playerScript.TransferValue(playerScript.playerValue);
                    UpdateTextMesh();
                    LoadNextScene(); // Only load the next scene if the action is correct
                }
                else
                {
                    //Game_Manager.Instance.ShowError();
                    Panel.SetActive(false); // Keep the panel inactive if the action is wrong
                }
            }
            else
            {
                Game_Manager.Instance.ShowError1();
            }
        }
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

