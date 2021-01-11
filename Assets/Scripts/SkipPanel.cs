using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipPanel : MonoBehaviour
{
    public GameObject skipButton;

    public float scaleSpeed = 5f;

    public void Open ()
    {
        gameObject.SetActive(true);

        StartCoroutine(Open(scaleSpeed));

        skipButton.SetActive(false);
    }

    public void Close ()
    {
        StartCoroutine(Close(scaleSpeed));

        skipButton.SetActive(true);
    }

    IEnumerator Open (float speed)
    {
        float s = 0f;

        while (s < 1f)
        {
            s += Time.deltaTime * speed;

            transform.localScale = new Vector3(1f, s, 1f);

            yield return null;
        }

        transform.localScale = Vector3.one;
    }

    IEnumerator Close(float speed)
    {
        float s = 1f;

        while (s > 0)
        {
            s -= Time.deltaTime * speed;

            transform.localScale = new Vector3(1f, s, 1f);

            yield return null;
        }

        transform.localScale = new Vector3(1f, 0f, 1f);

        gameObject.SetActive(false);
    }
}
