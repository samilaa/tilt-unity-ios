using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTransform;
    private Quaternion originalRotation;

    private void Start()
    {
        camTransform = Camera.main.transform;

        originalRotation = transform.rotation;
    }

    private void Update()
    {
        //camTransform = Camera.main.transform;
        transform.rotation = camTransform.rotation * originalRotation;
    }
}
