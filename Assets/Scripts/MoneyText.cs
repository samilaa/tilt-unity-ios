using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Text>().text = "$ " + Wallet.GetCoinAmount().ToString();
    }
}
