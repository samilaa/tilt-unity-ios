using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Wallet");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public static int GetCoinAmount()
    {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    public static bool CanBuy (int price)
    {
        if (GetCoinAmount() >= price)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void Transaction (int amount)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + amount);

        if (GameObject.Find ("WalletText"))
        {
            GameObject.Find("WalletText").GetComponent<WalletText>().Transaction(amount);
        }
    }
}
