using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public GameObject settings;

    public GameObject slider;

    private void Start()
    {
        slider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sensitivity", 100f);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("Level", 0);

        CharacterManager cm = GameObject.FindWithTag("CharacterManager").GetComponent<CharacterManager>();
        cm.Refresh();

        for (int i = 1; i < cm.isLocked.Length; i++)
        {
            string key = i.ToString() + "_islocked";

            PlayerPrefs.SetInt(key, 1);

            cm.Refresh();
        }

        PlayerPrefs.SetInt("Character", 0);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToggleSettings ()
    {
        if (settings.activeInHierarchy)
            settings.SetActive(false);
        else
            settings.SetActive(true);
    }

    public void SetSensitivity()
    {
        float s = slider.GetComponent<Slider>().value;

        PlayerPrefs.SetFloat("Sensitivity", s);
    }
}
