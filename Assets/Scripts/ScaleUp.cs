using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    public float speed;

    public bool x;
    public bool y;
    public bool z;

    private void Start()
    {
        StartCoroutine(DoScaleUp());
    }

    IEnumerator DoScaleUp ()
    {
        float s = 0f;

        while (s < 1f)
        {
            s += Time.deltaTime * speed;

            float xScale;
            float yScale;
            float zScale;

            if (x)
                xScale = s;
            else
                xScale = 1f;
            if (y)
                yScale = s;
            else
                yScale = 1f;
            if (z)
                zScale = s;
            else
                zScale = 1f;

            transform.localScale = new Vector3(xScale, yScale, zScale);

            yield return null;
        }

        transform.localScale = Vector3.one;
    }
}
