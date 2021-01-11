using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimator : MonoBehaviour
{
    // Elements to animate
    public GameObject[] textFadeFromUp;
    public GameObject[] scaleUpX;

    // Variables
    public float moveAmountFadeUp;
    public float fadeTime;
    public float interval;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            DoFadeUp();
    }

    private void DoFadeUp ()
    {
        foreach (GameObject item in textFadeFromUp)
        {
            StartCoroutine(MoveUp(item, true, moveAmountFadeUp, fadeTime));
            StartCoroutine(TextFade(item.GetComponent<Text>(), true, fadeTime));
        }
    }

    IEnumerator MoveUp (GameObject go, bool from, float amount, float time)
    {
        Vector3 upPos = go.transform.position + Vector3.up * amount;
        Vector3 targetPos = go.transform.position;
        float speed = 1f / time;

        float delta = Mathf.Abs (targetPos.y - upPos.y);
        float t = 0f;

        if (from)
        {
            while (t < time)
            {
                go.transform.position = upPos - Vector3.up * delta * t;

                t += Time.deltaTime;

                yield return null;
            }

            go.transform.position = targetPos;
        }

        else
        {
            while (t < time)
            {
                go.transform.position = targetPos + Vector3.up * delta * t;

                t += Time.deltaTime;

                yield return null;
            }

            go.transform.position = upPos;
        }

    }

    IEnumerator TextFade (Text text, bool fadeIn, float time)
    {
        float t = 0f;

        float speed = 1f / time;

        float o;
        if (fadeIn)
            o = 0f;
        else
            o = 1f;

        if (fadeIn)
        {
            while (o < 1f)
            {
                Color col = text.color;
                col.a = o;

                o += Time.deltaTime * speed;

                yield return null;
            }
        }
        else
        {
            while (o > 0f)
            {
                Color col = text.color;
                col.a = o;

                o -= Time.deltaTime * speed;

                yield return null;
            }
        }
    }
}
