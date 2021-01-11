using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    public GameObject[] levels;
    private GameObject currentLevel;

    public GameObject finishScreen;
    private GameObject currentFinishScreen;

    public bool finished;

    public GameObject player;
    public GameObject gameOverMenu;

    private GameObject activeMenu;

    public float defaultFOV;

    public GameObject mainMenu;

    public GameObject fadeOut;

    public GameObject winAPrize;

    public GameObject inGameUI;

    public AdsManager adsManager;

    public GameObject levelText;

    private CharacterManager cm;

    private void Start()
    {
        cm = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();

        Application.targetFrameRate = 60;

        LoadLevel();

        //StartCoroutine(SetCameraFOV(Camera.main, defaultFOV, 100f));
    }

    public void StartGame ()
    {
        //LoadLevel();
        SpawnPlayer();
        currentLevel.GetComponent<TestPlatformController>().enabled = true;
        currentLevel.GetComponent<TestPlatformController>().rotSpeed = PlayerPrefs.GetFloat("Sensitivity", 100f);

        mainMenu.SetActive(false);
        inGameUI.SetActive(true);

        StartCoroutine(SetCameraFOV(Camera.main, defaultFOV, 100f));
    }

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown (KeyCode.A))
        {
            //PlayerPrefs.DeleteAll();
            //Wallet.Transaction(1000);

            /*for (int i = 1; i < cm.characters.Length; i++)
            {
                string key = i.ToString() + "_islocked";

                PlayerPrefs.SetInt(key, 0);
            }*/

            if (PlayerPrefs.GetInt ("Level", 0) > 0)
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt ("Level", 0) - 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (PlayerPrefs.GetInt("Level", 0) < levels.Length - 1)
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 0) + 1);
        }
    }

    public int GetLevelCompleteCoins ()
    {
        return 10;
    }

    public void GameOver ()
    {
        StartCoroutine (SetCameraFOV(Camera.main, 120f, 100f));

        //LoadMenuView();
        activeMenu = Instantiate(gameOverMenu, GameObject.FindWithTag("MainCanvas").transform.Find("GameOverMenuHolder"));

        // Handled in "GameOverManager"
        /*if (Wallet.GetCoinAmount() >= 100)
            Instantiate(winAPrize, activeMenu.transform);*/
    }

    public void LoadMenuView()
    {
        GameObject fadeInstance = Instantiate(fadeOut, GameObject.FindWithTag("MainCanvas").transform);
        fadeInstance.GetComponent<Image>().color = GetComponent<ThemeManager>().skyboxes[GetComponent<ThemeManager>().GetThemeID()];

        if (GameObject.FindWithTag("WinAPrize") != null)
            Destroy(GameObject.FindWithTag("WinAPrize"));

        mainMenu.SetActive(true);
        inGameUI.SetActive(false);
        StartCoroutine(SetCameraFOV(Camera.main, 150f, 100f));
    }

    IEnumerator SetCameraFOV(Camera cam, float target, float speed)
    {
        float s = cam.fieldOfView;

        bool up;

        if (target > s)
            up = true;
        else
            up = false;

        yield return null;

        if (up)
        {
            while (s < target)
            {
                s += Time.deltaTime * speed;

                cam.fieldOfView = s;

                yield return null;
            }
        }
        else
        {
            while (s > target)
            {
                s -= Time.deltaTime * speed;

                cam.fieldOfView = s;

                yield return null;
            }
        }

        yield return null;

        cam.fieldOfView = target;
    }

    public void FinishLevel ()
    {
        if (!finished)
        {
            StartCoroutine(SetCameraFOV(Camera.main, 120f, 100f));

            Transform mainCanvas = GameObject.FindWithTag("MainCanvas").transform;

            activeMenu = Instantiate(finishScreen, mainCanvas);

            inGameUI.SetActive(false);

            // Save analytics
            string eventName = "Level" + PlayerPrefs.GetInt("Level", 0).ToString();
            Analytics.CustomEvent(eventName);

            if (PlayerPrefs.GetInt("Level", 0) < levels.Length - 1)
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 0) + 1);
            else
                Debug.Log("Max level reached!");

            Debug.Log("Level " + PlayerPrefs.GetInt("Level", 0));

            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Success);
            AudioManager.instance.Play("Bleep");

            // Add coins!
            Wallet.Transaction(GetLevelCompleteCoins ());

            finished = true;

            /*if (Wallet.GetCoinAmount() >= 100)
                Instantiate(winAPrize, mainCanvas);*/
        }
    }

    public void LoadLevel ()
    {
        // Refresh characters
        cm.Refresh();

        // Go to main menu setting
        LoadMenuView();

        // Run ads if count is right
        if (adsManager != null)
            adsManager.PlayAd();

        // Destroy previous level
        if (currentLevel != null)
        {
            Debug.Log("Current level is: " + currentLevel.name);

            Destroy(currentLevel);
        }

        AudioManager.instance.Play("BassBend");

        finished = false;

        int level = PlayerPrefs.GetInt("Level", 0);

        levelText.GetComponent<LevelText>().UpdateText();

        Debug.Log("Level to be loaded is Level" + level);

        currentLevel = Instantiate(levels[level]);
        currentLevel.GetComponent<TestPlatformController>().enabled = false;

        /*// Hide the finish screen
        if (currentFinishScreen != null)
            Destroy(currentFinishScreen);*/

        //SpawnPlayer();

        //StartCoroutine(SetCameraFOV(Camera.main, 90f, 100f));

        if (activeMenu != null && activeMenu.GetComponent<Animator>() != null)
            activeMenu?.GetComponent<Animator>()?.SetTrigger("Out");

        Destroy(activeMenu);

        ThemeManager tm = GetComponent<ThemeManager>();
        tm.LoadTheme(tm.GetThemeID());
    }

    public void SpawnPlayer ()
    {
        // Spawn new player
        if (GameObject.FindWithTag("Player") != null)
            Destroy(GameObject.FindWithTag("Player"));

        //Instantiate(player, currentLevel.transform.Find("PlayerSpawn").position, Quaternion.identity);
        GetComponent<CharacterSelector>().SpawnCharacter(currentLevel.transform.Find("PlayerSpawn").position);
    }

    public void LoadSceneOfName (string name)
    {
        SceneManager.LoadScene(name);
    }
}
