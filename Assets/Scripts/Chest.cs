using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    private CharacterManager cm;

    public float animationLength = 2f;

    private GameObject currentChar;

    public Transform ballSpot;
    public Transform ballSpot2;

    private bool opened;

    public GameObject priceText;
    public Text topText;

    public int priceToOpen;

    public GameObject[] unlockMenus;
    private GameObject currentUnlockMenu;

    private int _char = 0;

    private void Start()
    {
        cm = GameObject.FindWithTag("CharacterManager").GetComponent<CharacterManager>();

        opened = false;

        if (Wallet.GetCoinAmount() < priceToOpen)
            topText.text = "insufficient money!";

        priceText.GetComponent<Text>().text = priceToOpen.ToString() + " $";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Open();

        if (Input.GetKeyDown (KeyCode.A))
        {
            Wallet.Transaction(1000);
        }
    }

    public void ShowUnlockMenu (int id)
    {
        currentUnlockMenu = Instantiate(unlockMenus[id], GameObject.FindWithTag("MainCanvas").transform);

        if (id == 0)
        {
            currentUnlockMenu.transform.Find("ButtonRight").GetComponent<Button>().onClick.AddListener(LoadMenu);
        }
        else if (id == 1)
        {
            currentUnlockMenu.transform.Find("Text").GetComponent<Text>().text = "already unlocked";
        }
    }

    private void LoadMenu ()
    {
        PlayerPrefs.SetInt("Character", _char);

        SceneManager.LoadScene("Game");
    }

    private void LoadThis ()
    {
        SceneManager.LoadScene("ChestOpening");
    }

    private void OnMouseDown()
    {
        if (!opened && Wallet.GetCoinAmount() >= priceToOpen)
            Open();
        else if (!opened)
        {
            //topText.text = "insufficient money!";
        }
    }

    public void Open ()
    {
        GetComponent<Animator>().SetTrigger("Open");

        int rand = 1;

        while (rand == PlayerPrefs.GetInt ("LastChest", 0))
        {
            rand = Mathf.RoundToInt(Random.Range(1f, cm.characters.Length - 1));
        }

        PlayerPrefs.SetInt("LastChest", rand);

        _char = rand;

        opened = true;

        priceText.SetActive(false);

        OnOpen();

        StartCoroutine(InstantiateAfterSeconds(rand, animationLength));
    }

    public void OnOpen()
    {
        Wallet.Transaction(-priceToOpen);

        /*if (Wallet.GetCoinAmount() < priceToOpen)
            topText.text = "insufficient money!";*/
    }

    public void DoHaptic (string type)
	{
        if (type == "Open")
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Success);
        if (type == "Medium")
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
        if (type == "Heavy")
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
    }

    public void Unlock(int character)
    {
        string key = character.ToString() + "_islocked";

        int l = PlayerPrefs.GetInt(key, 1);

        if (l == 1)
        {
            PlayerPrefs.SetInt(key, 0);
            cm.Refresh();

            ShowUnlockMenu(0);
        }

        else
        {
            //Wallet.Transaction(30);

            Debug.Log("Character " + character + " is already unlocked!");

            ShowUnlockMenu(1);
        }
    }

    public void PlaySound (string sound)
    {
        AudioManager.instance.Play(sound);
    }

    IEnumerator InstantiateAfterSeconds (int charID, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        currentChar = Instantiate(cm.characters[charID], ballSpot.position, Quaternion.identity);
        currentChar.GetComponent<Player>().enabled = false;
        currentChar.GetComponent<Rigidbody>().useGravity = false;

        Unlock(charID);

        StartCoroutine(MoveToNewSpot(currentChar.transform, ballSpot2.position, 2f));
        StartCoroutine(ScaleTo(currentChar.transform, 2f, 0.75f));
        StartCoroutine(SpinToRot(currentChar.transform, new Vector3 (-270f, 240, 120f), 2f));
    }

    IEnumerator ScaleTo(Transform obj, float scale, float speed)
    {
        float s = obj.localScale.x;

        while (s < scale)
        {
            s += Time.deltaTime * speed;

            obj.localScale = Vector3.one * s;

            yield return null;
        }

        obj.localScale = Vector3.one * scale;
    }

    IEnumerator MoveToNewSpot (Transform obj, Vector3 spot, float speed)
    {
        float d = Vector3.Distance(obj.position, spot);

        while (d > 0.1f)
        {
            obj.position = Vector3.Lerp(obj.position, spot, Time.deltaTime * speed);

            d = Vector3.Distance(obj.position, spot);

            yield return null;
        }
    }

    IEnumerator SpinToRot(Transform obj, Vector3 rot, float speed)
    {
        float d = Vector3.Distance(obj.rotation.eulerAngles, rot);

        while (d > 0.1f)
        {
            obj.rotation = Quaternion.Lerp(obj.rotation, Quaternion.Euler(rot), Time.deltaTime * speed);

            d = Vector3.Distance(obj.rotation.eulerAngles, rot);

            yield return null;
        }
    }
}
