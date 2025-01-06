//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using TMPro; // Add this line

//public class IR_behaviour : MonoBehaviour
//{
//    public static int value = 0;
//    private TextMeshPro textMesh;
//    public GameObject Panel;
//    private Player_behaviour playerScript;
//    //public InputField digitCountField; // Assign in Unity Editor
//    public InputField digitPositionField; // Assign in Unity Editor
//    private Address_1 address1Script;
//    private Address_2 address2Script;
//    private Address_3 address3Script;
//    private Address_4 address4Script;
//    private Address_5 address5Script;

//    void Start()
//    {
//        textMesh = GetComponent<TextMeshPro>();
//        address1Script = FindObjectOfType<Address_1>(); // Assign the Address_1 script instance
//        address2Script = FindObjectOfType<Address_2>(); // Assign the Address_2 script instance
//        address3Script = FindObjectOfType<Address_3>(); // Assign the Address_3 script instance
//        address4Script = FindObjectOfType<Address_4>(); // Assign the Address_4 script instance
//        address5Script = FindObjectOfType<Address_5>(); // Assign the Address_5 script instance
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
//            textMesh.text = "IR:\t" + text.PadLeft(totalWidth);
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
//            digitPositionField.text = "";
//            playerScript = other.gameObject.GetComponent<Player_behaviour>();
//        }
//    }

//    public void OnCollectButton()
//    {
//        if (digitPositionField.text.Length > 0)
//        {
//            string valueString = IR_behaviour.value.ToString("D4");
//            string positions = digitPositionField.text;
//            int collectedValue = 0;

//            foreach (char pos in positions)
//            {
//                int position = (int)char.GetNumericValue(pos) - 1; // Adjust for zero-based index
//                if (position >= 0 && position < valueString.Length)
//                {
//                    int digit = (int)char.GetNumericValue(valueString[position]);
//                    collectedValue = collectedValue * 10 + digit; // Append digit
//                }
//                else
//                {
//                    Debug.LogWarning("Position out of range: " + position);
//                    // Handle invalid position
//                }
//            }

//            if (playerScript != null)
//            {
//                if (Game_Manager.Instance.expectedAction == "MAR<-IR[11-0]")
//                {
//                    playerScript.CollectValue(collectedValue);
//                }
//                else
//                {
//                    Game_Manager.Instance.ShowError1();
//                }
//            }
//        }

//        Panel.SetActive(false);
//    }


//    public void OnTransferButton()
//    {
//        if (playerScript != null)
//        {
//            if (Game_Manager.Instance.expectedAction == "IR<-M[MAR]")
//            {
//                if(MAR_behaviour.value == address1Script.value1 && playerScript.playerValue != address1Script.value2)
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
//                IR_behaviour.value = playerScript.playerValue; // Transfer player's value to IR
//                playerScript.TransferValue(playerScript.playerValue); // Subtract the transferred value from player
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
//        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
//        SceneManager.LoadScene(currentSceneIndex + 1);
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Add this line

public class IR_behaviour : MonoBehaviour
{
    public static int value = 0;
    private TextMeshPro textMesh;
    public GameObject Panel;
    private Player_behaviour playerScript;
    public InputField digitPositionField; // Assign in Unity Editor
    private demo[] addresses; // Reference to all address instances

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        addresses = FindObjectsOfType<demo>(); // Find all address instances dynamically
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
            textMesh.text = "IR:\t" + text.PadLeft(totalWidth);
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
            digitPositionField.text = "";
            playerScript = other.gameObject.GetComponent<Player_behaviour>();
        }
    }

    public void OnCollectButton()
    {
        if (digitPositionField.text.Length > 0)
        {
            string valueString = IR_behaviour.value.ToString("D4");
            string positions = digitPositionField.text;
            int collectedValue = 0;

            foreach (char pos in positions)
            {
                int position = (int)char.GetNumericValue(pos) - 1; // Adjust for zero-based index
                if (position >= 0 && position < valueString.Length)
                {
                    int digit = (int)char.GetNumericValue(valueString[position]);
                    collectedValue = collectedValue * 10 + digit; // Append digit
                }
                else
                {
                    Debug.LogWarning("Position out of range: " + position);
                    // Handle invalid position
                }
            }

            if (playerScript != null)
            {
                if (Game_Manager.Instance.expectedAction == "MAR<-IR[11-0]")
                {
                    playerScript.CollectValue(collectedValue);
                }
                else
                {
                    Game_Manager.Instance.ShowError1();
                }
            }
        }

        Panel.SetActive(false);
    }

    public void OnTransferButton()
    {
        if (playerScript != null)
        {
            if (Game_Manager.Instance.expectedAction == "IR<-M[MAR]")
            {
                demo targetAddress = FindMatchingAddress();
                if (targetAddress == null || playerScript.playerValue != targetAddress.value2)
                {
                    // The player performed the wrong action
                    Game_Manager.Instance.ShowError();
                    Panel.SetActive(false);
                    return; // Exit the method without changing the scene
                }
                IR_behaviour.value = playerScript.playerValue; // Transfer player's value to IR
                playerScript.TransferValue(playerScript.playerValue); // Subtract the transferred value from player
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
