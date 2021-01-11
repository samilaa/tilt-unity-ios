using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    public SimpleScrollSnap simpleSnap;

    private CharacterManager cm;

    public GameObject selectButton;

    public GameObject viewport;
    public GameObject backgroundPanel;
    public GameObject tapToCustomize;

    private bool entireSelectionOpen;

    private void Start()
    {
        //simpleSnap.GoToPanel(2);
        StartCoroutine(DoStart());

        CloseEntireSelection();

        Debug.Log("Current character ID: " + PlayerPrefs.GetInt("Character", 0));

        cm = GameObject.FindWithTag("CharacterManager").GetComponent<CharacterManager>();
    }

    IEnumerator DoStart ()
    {
        yield return null;

        simpleSnap.GoToPanel(PlayerPrefs.GetInt("Character", 0));
    }

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.A))
        {
            simpleSnap.GoToPanel(0);
        }

        int selectedChar = simpleSnap.TargetPanel;

        if (cm.isLocked[selectedChar])
        {
            selectButton.GetComponent<Button>().interactable = false;
        }
        else if (!cm.isLocked[selectedChar])
        {
            selectButton.GetComponent<Button>().interactable = true;
        }
    }

    public void ToggleEntireSelection ()
    {
        if (!entireSelectionOpen)
            OpenEntireSelection();
        else if (!cm.isLocked[simpleSnap.CurrentPanel])
        {
            CloseEntireSelection();

            PlayerPrefs.SetInt("Character", simpleSnap.CurrentPanel);
        }
    }

    public void OpenEntireSelection()
    {
        viewport.GetComponent<Mask>().enabled = false;
        //viewport.GetComponent<Button>().interactable = false;
        simpleSnap.swipeGestures = true;

        StartCoroutine(Scale(backgroundPanel, true, 4f));

        selectButton.SetActive(true);
        tapToCustomize.SetActive(false);

        entireSelectionOpen = true;
    }

    public void CloseEntireSelection()
    {
        viewport.GetComponent<Mask>().enabled = true;
        //viewport.GetComponent<Button>().interactable = true;
        simpleSnap.swipeGestures = false;

        StartCoroutine(Scale(backgroundPanel, false, 4f));

        selectButton.SetActive(false);
        tapToCustomize.SetActive(true);

        entireSelectionOpen = false;
    }

    IEnumerator Scale (GameObject go, bool up, float speed)
    {
        float s;

        if (up)
        {
            s = go.transform.localScale.x;

            while (s < 1f)
            {
                s += Time.deltaTime * speed;
                go.transform.localScale = new Vector3(s, 1f, 1f);

                yield return null;
            }

            go.transform.localScale = Vector3.one;
        }

        else
        {
            s = go.transform.localScale.x;

            while (s > 0f)
            {
                s -= Time.deltaTime * speed;
                go.transform.localScale = new Vector3(s, 1f, 1f);

                yield return null;
            }

            go.transform.localScale = new Vector3(0f, 1f, 1f);
        }
    }

    public void SelectCharacter ()
    {
        SetCharacterPref(simpleSnap.TargetPanel);

        Debug.Log("Current character ID: " + PlayerPrefs.GetInt("Character", 0));
    }

    private void SetCharacterPref (int id)
    {
        PlayerPrefs.SetInt("Character", id);
    }
}
