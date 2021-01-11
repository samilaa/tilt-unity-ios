using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleIntroducer : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject square;

    public GameObject holder;

    public GameObject[] objectsToScale;

    public float squareRotateSpeed = 30f;

    private Transform camTransform;

    Quaternion originalRotation;

    [HideInInspector]
    public Transform player;

    private bool closed = false;

    public float minDistance = 0.4f;
    public float maxDistance = 3.5f;

    void Start()
    {
        camTransform = Camera.main.transform;

        originalRotation = transform.rotation;

        //player = GameObject.FindWithTag("Player").transform;

        closed = false;
    }

    void Update()
    {
        if (!closed && player != null)
        {
            transform.rotation = camTransform.rotation * originalRotation;

            square.transform.Rotate(Vector3.forward * squareRotateSpeed * Time.deltaTime);

            float d = Vector3.Distance(square.transform.position, player.transform.position) - 1f;

            holder.transform.localScale = Vector3.one * d * 0.5f;

            if (holder.transform.localScale.x < minDistance || holder.transform.localScale.x > maxDistance)
            {
                StartCoroutine(Close(holder.transform, 1.5f));
            }
        }
    }

    IEnumerator DoStart ()
    {
        float t = 0f;

        while (player == null || t < 5f)
        {
            player = GameObject.FindWithTag("Player").transform;
            t += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator Close(Transform t, float speed)
    {
        closed = true;

        float s = t.localScale.x;
        float speedMultiplier = s;

        while (s > 0f)
        {
            s -= Time.deltaTime * speed * speedMultiplier;

            t.localScale = Vector3.one * s;

            yield return null;
        }

        s = 0f;
        t.localScale = Vector3.one * s;
    }
}
