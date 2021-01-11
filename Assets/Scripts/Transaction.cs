using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transaction : MonoBehaviour
{
    public float moveDownAmount = 100f;
    public float speed;

    private Vector3 moveDownSpot;

    private float a;

    private void Start()
    {
        moveDownSpot = transform.position + Vector3.down * moveDownAmount;

        a = 1f;
    }

    private void Update()
    {
        float d = Vector3.Distance(transform.position, moveDownSpot);

        if (a > 0.01f)
        {
            a -= Time.deltaTime;

            Color col = GetComponent<Text>().color;
            col.a = a;

            GetComponent<Text>().color = col;
        }

        else
        {
            Destroy(gameObject);
        }

        if (d > 1f)
        {
            transform.position = Vector3.Lerp(transform.position, moveDownSpot, Time.deltaTime * speed);
        }
    }

    public void SetText (int amount)
    {
        GetComponent<Text>().text = amount.ToString() + " $";

        if (amount > 0)
        {
            GetComponent<Text>().color = Color.green;
        }
        else
        {
            GetComponent<Text>().color = Color.red;
        }
    }
}
