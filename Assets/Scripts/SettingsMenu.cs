using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public float scaleSpeed = 5f;

    public void Open()
    {
        gameObject.SetActive(true);

        StartCoroutine(Open(scaleSpeed));
    }

    public void Close()
    {
        StartCoroutine(Close(scaleSpeed));
    }

    IEnumerator Open(float speed)
    {
        float s = 0f;

        while (s < 1f)
        {
            s += Time.deltaTime * speed;

            transform.localScale = Vector3.one * s;

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

            transform.localScale = Vector3.one * s;

            yield return null;
        }

        transform.localScale = new Vector3(0f, 0f, 0f);

        gameObject.SetActive(false);
    }
}
