using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlatformController : MonoBehaviour
{
    private Vector3 startTouch;
    private Vector3 dir;

    [HideInInspector]
    public float rotSpeed = 120f; // default 120
    private float rotSnapSpeed = 9f; // default 10

    private float maxAngle = 30f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rotSpeed = PlayerPrefs.GetFloat("Sensitivity", 100f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown (0))
        {
            startTouch = Input.mousePosition;
        }

        if (Input.GetMouseButton (0))
        {
            dir = Input.mousePosition - startTouch;
        }

        if (Input.GetMouseButtonUp (0))
        {
            startTouch = Vector3.zero;
            dir = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        Vector3 dirNormalized = new Vector3(dir.x / Screen.width, dir.y / Screen.width, 0f);
        Vector3 dirDamped = dirNormalized * rotSpeed;

        float xRot = Mathf.Clamp(dirDamped.y, -maxAngle, maxAngle);
        float yRot = Mathf.Clamp(-dirDamped.x, -maxAngle, maxAngle);

        Vector3 rotVector = new Vector3(xRot, 0f, yRot);

        Quaternion rot = Quaternion.Euler(rotVector);

        Quaternion smoothRot = Quaternion.Lerp(transform.rotation, rot, rotSnapSpeed * Time.deltaTime);

        Vector3 r = transform.rotation.eulerAngles - rotVector;

        //Debug.Log(r);
        Quaternion rq = Quaternion.Euler(r);

        rb.rotation = smoothRot;
    }
}
