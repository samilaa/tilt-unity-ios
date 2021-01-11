using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    public float alpha;

    private void Start()
    {
        StartCoroutine(DoStart());
    }

    IEnumerator DoStart ()
    {
        yield return null;

        MakeTransparent();
    }

    private void MakeTransparent ()
    {
        Color col = GetComponent<Renderer>().material.color;
        col.a = alpha;
        GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
        //GetComponent<Renderer>().material.
        GetComponent<Renderer>().material.color = col;
    }
}
    