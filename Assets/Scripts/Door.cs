using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Image img;
    public Sprite[] sprites;

    public Transform holder;

    private Transform player;

    public float distanceToOpen;

    public GameObject particles;

    public GameObject[] destroyWith;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    private void Update()
    {
        float d = 0f;

        if (player != null)
            d = Vector3.Distance(holder.position, player.position);

        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;

        if (d < 3f && d >= distanceToOpen)
        {
            holder.localScale = Vector3.one * (-2 * d / 3f + 2f);
        }
        else if (d < distanceToOpen)
        {
            if (player != null && player.GetComponent<Player>().hasKey)
                OpenDoor();
            else if (player != null)
                Debug.Log("Player doesn't have the key!");
        }
        else
        {
            holder.localScale = Vector3.zero;
        }

        if (player != null && player.GetComponent<Player>().hasKey)
            img.sprite = sprites[0];
        else
            img.sprite = sprites[1];

    }

    public void OpenDoor ()
    {
        AudioManager.instance.Play("Puff");
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);

        Instantiate(particles, transform.position, Quaternion.identity);

        if (destroyWith.Length > 0)
        {
            foreach (GameObject go in destroyWith)
            {
                Instantiate(particles, go.transform.position, Quaternion.identity);

                go.SetActive(false);
            }
        }

        gameObject.SetActive(false);
    }
}
