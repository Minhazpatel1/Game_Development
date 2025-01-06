//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using TMPro;

//public class MBR_behaviour : MonoBehaviour
//{

//    public static int value = 0;
//    private TextMeshPro textMesh;
//    public GameObject Panel;
//    private Player_behaviour playerScript;
//    private Address_1 address1Script;
//    private Address_2 address2Script;
//    private Address_3 address3Script;
//    private Address_4 address4Script;
//    private Address_5 address5Script;

//    void Start()
//    {
//        address1Script = FindObjectOfType<Address_1>(); // Assign the Address_1 script instance
//        address2Script = FindObjectOfType<Address_2>(); // Assign the Address_2 script instance
//        address3Script = FindObjectOfType<Address_3>(); // Assign the Address_3 script instance
//        address4Script = FindObjectOfType<Address_4>(); // Assign the Address_4 script instance
//        address5Script = FindObjectOfType<Address_5>(); // Assign the Address_5 script instance
//        textMesh = GetComponent<TextMeshPro>();
//        UpdateTextMesh();
//        Panel.SetActive(false); // Hide the panel initially
//    }

//    void UpdateTextMesh()
//    {
//        if (textMesh != null)
//        {
//            int totalWidth = 5;
//            string valueText = value.ToString("D4");
//            string text = valueText;
//            textMesh.text = "MBR:\t" + text.PadLeft(totalWidth);
//        }
//        else
//        {
//            Debug.LogWarning("TextMesh component not found on the game object!");
//        }
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            Panel.SetActive(true); // Show the panel
//            playerScript = other.gameObject.GetComponent<Player_behaviour>();
//        }
//    }

//    public void OnCollectButton()
//    {
//        if (playerScript != null)
//        {
//            if (Game_Manager.Instance.expectedAction == "AC<-MBR")
//            {
//                playerScript.CollectValue(value);
//                UpdateTextMesh();
//            }
//            else
//            {
//                Game_Manager.Instance.ShowError1();
//            }
//        }
//        Panel.SetActive(false);
//    }

//    public void OnTransferButton()
//    {
//        if (playerScript != null)
//        {

//            if (Game_Manager.Instance.expectedAction == "MBR<-M[MAR]")
//            {
//                if (MAR_behaviour.value == address1Script.value1 && playerScript.playerValue != address1Script.value2)
//                {
//                    // The player performed the wrong action
//                    Game_Manager.Instance.ShowError();
//                    Panel.SetActive(false);
//                    return; // Exit the method without changing the scene
//                }
//                else if (MAR_behaviour.value == address2Script.value1 && playerScript.playerValue != address2Script.value2)
//                {
//                    // The player performed the wrong action
//                    Game_Manager.Instance.ShowError();
//                    Panel.SetActive(false);
//                    return; // Exit the method without changing the scene
//                }
//                else if (MAR_behaviour.value == address3Script.value1 && playerScript.playerValue != address3Script.value2)
//                {
//                    // The player performed the wrong action
//                    Game_Manager.Instance.ShowError();
//                    Panel.SetActive(false);
//                    return; // Exit the method without changing the scene
//                }
//                else if (MAR_behaviour.value == address4Script.value1 && playerScript.playerValue != address4Script.value2)
//                {
//                    // The player performed the wrong action
//                    Game_Manager.Instance.ShowError();
//                    Panel.SetActive(false);
//                    return; // Exit the method without changing the scene
//                }
//                else if (MAR_behaviour.value == address5Script.value1 && playerScript.playerValue != address5Script.value2)
//                {
//                    // The player performed the wrong action
//                    Game_Manager.Instance.ShowError();
//                    Panel.SetActive(false);
//                    return; // Exit the method without changing the scene
//                }
//                MBR_behaviour.value = playerScript.playerValue;
//                playerScript.TransferValue(playerScript.playerValue);
//                UpdateTextMesh();
//                Game_Manager.Instance.AddScore(100);
//                LoadNextScene();
//            }
//            else
//            {
//                Game_Manager.Instance.ShowError1();
//            }

//        }
//        Panel.SetActive(false);
//    }

//    public void OnCancelButton()
//    {
//        Panel.SetActive(false);
//    }

//    private void LoadNextScene()
//    {
//        if (Game_Manager.Instance.expectedAction == "AC<-AC+MBR" || 
//            Game_Manager.Instance.expectedAction == "AC<-MBR" ||
//            Game_Manager.Instance.expectedAction == "AC<-AC-MBR" ||
//            Game_Manager.Instance.expectedAction == "M[MAR] < -MBR")
//        {
//            SceneManager.LoadScene(8);
//        }
//        else
//        {
//            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
//            SceneManager.LoadScene(currentSceneIndex + 1);
//        }

//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics;

public class MBR_behaviour : MonoBehaviour
{
    public static int value = 0;
    private TextMeshPro textMesh;
    public GameObject Panel;
    private Player_behaviour playerScript;
    private demo[] addresses; // Dynamically handles all address instances

    void Start()
    {
        addresses = FindObjectsOfType<demo>(); // Find all address instances dynamically
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
            textMesh.text = "MBR:\t" + text.PadLeft(totalWidth);
        }
        else
        {
            UnityEngine.Debug.LogWarning("TextMesh component not found on the game object!");
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
            if (Game_Manager.Instance.expectedAction == "AC<-MBR")
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
            if (Game_Manager.Instance.expectedAction == "MBR<-M[MAR]")
            {
                demo targetAddress = FindMatchingAddress();
                if (targetAddress == null || playerScript.playerValue != targetAddress.value2)
                {
                    // The player performed the wrong action
                    Game_Manager.Instance.ShowError();
                    Panel.SetActive(false);
                    return; // Exit the method without changing the scene
                }
                MBR_behaviour.value = playerScript.playerValue;
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

    private demo FindMatchingAddress()
    {
        foreach (demo address in addresses)
        {
            if (MAR_behaviour.value == address.value1)
            {
                return address;
            }
        }
        return null; // No matching address found
    }

    public void OnCancelButton()
    {
        Panel.SetActive(false);
    }

    private void LoadNextScene()
    {
        if (Game_Manager.Instance.expectedAction == "AC<-AC+MBR" ||
            Game_Manager.Instance.expectedAction == "AC<-MBR" ||
            Game_Manager.Instance.expectedAction == "AC<-AC-MBR" ||
            Game_Manager.Instance.expectedAction == "M[MAR]<-MBR")
        {
            SceneManager.LoadScene(8);
        }
        else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
