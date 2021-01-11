using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject slider;

    private bool characterSelectionOpen;

    public GameObject characterSelectionScreen;

    private void Start()
    {
        Application.targetFrameRate = 60;

        slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sensitivity", 100f);

        characterSelectionOpen = false;
    }

    private void Awake()
    {
        PlayerPrefs.SetInt("0_islocked", 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToggleCharacterSelection ()
    {
        bool open = characterSelectionScreen.activeInHierarchy;

        if (!open)
            characterSelectionScreen.SetActive(true);
        else
        {
            characterSelectionScreen.SetActive(false);
        }
    }

    public void LoadGame ()
    {
        SceneManager.LoadScene("Game");
    }

    public void ResetProgress ()
    {
        PlayerPrefs.SetInt("Level", 0);

        for (int i = 1; i < GameObject.FindWithTag ("CharacterManager").GetComponent<CharacterManager>().isLocked.Length; i++)
        {
            string key = i.ToString() + "_islocked";

            PlayerPrefs.SetInt(key, 1);
        }
    }

    public void SetSensitivity ()
    {
        float s = slider.GetComponent<Slider>().value;

        PlayerPrefs.SetFloat("Sensitivity", s);
    }
}
