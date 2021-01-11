using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMorph : MonoBehaviour
{
    public GameObject skin;

    private Rigidbody rb;

    private Vector3 lastVelocity;

    private Vector3 acceleration;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        skin = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        acceleration = (rb.velocity - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = rb.velocity;

        Debug.Log(acceleration);

        Vector3 scale = acceleration;

        skin.transform.localScale = scale;
    }
}
