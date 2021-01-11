using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characters;
    private CharacterManager characterManager;

    private void Start()
    {
        characterManager = GameObject.FindWithTag("CharacterManager").GetComponent<CharacterManager>();
    }

    public void SpawnCharacter (Vector3 pos)
    {
        int id = PlayerPrefs.GetInt("Character", 0);

        Instantiate(characterManager.characters[id], pos, Quaternion.identity);
    }
}
