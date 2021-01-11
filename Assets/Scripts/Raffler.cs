using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine.UI;

public class Raffler : MonoBehaviour
{
    public SimpleScrollSnap simpleSnap;
    private CharacterManager cm;

    public GameObject unlockedScreen;
    public GameObject alreadyUnlockedScreen;
    private GameObject currentScreen;

    private Transform mainCanvas;

    public bool spinning;

    public GameObject spinButton;

    private void Awake()
    {
        cm = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();

        mainCanvas = GameObject.FindWithTag("MainCanvas").transform;

        spinning = false;
    }

    private void Update()
    {
        //Debug.Log(simpleSnap.TargetPanel);

        if (Input.GetKeyDown (KeyCode.D))
        {
            for (int i = 1; i < cm.isLocked.Length; i++)
            {
                string key = i.ToString() + "_islocked";

                PlayerPrefs.SetInt(key, 1);
            }
        }

        if (Wallet.GetCoinAmount() >= 100)
            spinButton.GetComponent<Button>().interactable = true;
        else
            spinButton.GetComponent<Button>().interactable = false;
    }

    public void SetSpinningState ()
    {
        spinning = !spinning;
    }

    public void OnSpin ()
    {
        Wallet.Transaction(-100);
    }

    public void UnlockSelected ()
    {
        int character = simpleSnap.TargetPanel + 1;

        string key = character.ToString() + "_islocked";

        int l = PlayerPrefs.GetInt(key, 1);

        if (l == 1 && spinning)
        {
            PlayerPrefs.SetInt(key, 0);
            cm.Refresh();

            PlayerPrefs.SetInt("Character", character);
            ShowUnlockedScreen(character);
        }

        else if (spinning)
        {
            ShowAlreadyUnlockedScreen(character);

            Wallet.Transaction(30);

            Debug.Log("Character " + character + " is already unlocked!");
        }
    }

    public void ShowUnlockedScreen (int selectedID)
    {
        currentScreen = Instantiate(unlockedScreen, mainCanvas);
        currentScreen.transform.Find("CharacterImage").GetComponent<Image>().sprite = cm.characterImages[selectedID];

        SetSpinningState();
    }

    public void ShowAlreadyUnlockedScreen(int selectedID)
    {
        currentScreen = Instantiate(alreadyUnlockedScreen, mainCanvas);
        currentScreen.transform.Find("CharacterImage").GetComponent<Image>().sprite = cm.characterImages[selectedID];

        SetSpinningState();
    }
}
