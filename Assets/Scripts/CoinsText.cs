using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsText : MonoBehaviour
{
    private Text text;

    public float scaleTo = 1.25f;

    public int amount = 10;

    private void Start()
    {
        text = GetComponent<Text>();

        text.text = "+ " + amount.ToString() + " $";
    }

    public void Double ()
    {
        StartCoroutine(DoDouble(2f));
    }

    IEnumerator DoDouble (float speed)
    {
        float s = transform.localScale.x;

        amount = amount * 2;

        text.text = "+ " + amount.ToString() + " $";

        while (s < scaleTo)
        {
            s += Time.deltaTime * speed;

            transform.localScale = Vector3.one * s;

            yield return null;
        }
    }
}
