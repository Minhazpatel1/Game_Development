using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_behaviour : MonoBehaviour
{
    public int playerValue = 0;
    public void CollectValue(int value)
    {
        playerValue = value;
    }

    public void TransferValue(int value)
    {
        playerValue -= value;
    }
}