using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] characters;
    public Sprite[] characterImages;
    public bool[] isLocked;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("CharacterManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        Refresh();
    }

    public void Refresh ()
    {
        if (PlayerPrefs.GetInt("0_islocked", 0) == 1)
            PlayerPrefs.SetInt("0_islocked", 0);
        if (isLocked[0])
            isLocked[0] = false;

        for (int i = 1; i < isLocked.Length; i++)
        {
            string key = i.ToString() + "_islocked";
            if (PlayerPrefs.GetInt(key, 1) == 1)
            {
                isLocked[i] = true;
            }
            else
            {
                isLocked[i] = false;
            }
        }
    }
}
