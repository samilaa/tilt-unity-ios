using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalGameManager : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameManager")?.GetComponent<GameManager>();
    }

    public void LoadSceneOfName (string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadLevel ()
    {
        gm.LoadLevel();
    }
}
