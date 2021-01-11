using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletText : MonoBehaviour
{
    private Text text;

    public GameObject transaction;
    private GameObject current;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = Wallet.GetCoinAmount().ToString();
    }

    public void Transaction(int amount)
    {
        current = Instantiate(transaction, transform.position, Quaternion.identity, transform.parent);

        current.GetComponent<Transaction>().SetText(amount);
    }
}
