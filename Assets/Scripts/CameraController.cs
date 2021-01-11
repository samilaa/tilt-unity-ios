using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float maxDistanceDelta;
    public float moveSpeed;

    private Transform player;

    private Vector3 nextPosition;

    private void Start()
    {
        StartCoroutine(FindPlayer());

        nextPosition = transform.position + Vector3.forward * maxDistanceDelta;
    }

    private void LateUpdate()
    {
        float d = player.position.z - transform.position.z;

        if (d > maxDistanceDelta)
        {
            StartCoroutine (MoveTo(transform.position + Vector3.forward * maxDistanceDelta / 2f));

            Debug.Log("Player is far away from the Camera!");
        }
        if (d < -maxDistanceDelta)
        {
            StartCoroutine(MoveTo(transform.position - Vector3.forward * maxDistanceDelta / 2f));

            Debug.Log("Player is far away from the Camera!");
        }
    }

    IEnumerator MoveTo(Vector3 target)
    {
        float d = Vector3.Distance(transform.position, target);

        while (d > 0.02f)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * moveSpeed);

            d = Vector3.Distance(transform.position, target);

            yield return null;
        }

        transform.position = target;
    }

    IEnumerator FindPlayer ()
    {
        float t = 0f;

        while (player == null || t > 3f)
        {
            player = GameObject.FindWithTag("Player")?.transform;

            t += Time.deltaTime;

            yield return null;
        }
    }
}
