using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRotate : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 rot;
    public float speed;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();

        rot = Vector3.zero;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        /*rot += Vector3.up * speed * Time.fixedDeltaTime;

        rb.rotation = Quaternion.Euler(rot);*/
    }
}
