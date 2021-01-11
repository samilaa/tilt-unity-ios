using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject[] popUps;

    public float distance;

    private int popUpCount;

    private GameObject currentPopUp;

    public GameObject text;

    private void Start()
    {
        currentPopUp = text;

        popUpCount = 1;

        if (Wallet.GetCoinAmount() >= 100)
        {
            PopUp(popUps[0]);
        }

        GameManager gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        if (gm.finished)
        {
            PopUp(popUps[1]);
        }
    }

    private void PopUp (GameObject popUp)
    {
        //Vector3 pos = Vector3.down * distance * popUpCount + Vector3.up * Screen.height / 2f + Vector3.right * Screen.width / 2f;
        Vector3 pos = currentPopUp.transform.position + Vector3.down * distance * popUpCount + Vector3.down * currentPopUp.GetComponent<RectTransform>().sizeDelta.y / 2f;

        currentPopUp = Instantiate(popUp, pos, Quaternion.identity, transform);

        popUpCount += 1;
    }
}
