using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Text>().text = "Level " + PlayerPrefs.GetInt("Level", 0).ToString();
    }

    public void UpdateText ()
    {
        GetComponent<Text>().text = "Level " + PlayerPrefs.GetInt("Level", 0).ToString();
    }
}
