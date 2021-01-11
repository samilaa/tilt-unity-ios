using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public GameObject explosionEffect;

    private GameManager gm;

    private bool exploded;

    public bool hasKey;

    private bool canShake;

    private Rigidbody rb;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        exploded = false;

        rb = GetComponent<Rigidbody>();

        canShake = true;

        hasKey = false;

        GameObject[] obstacleIntroducers = GameObject.FindGameObjectsWithTag("ObstacleIntroducer");

        foreach (GameObject go in obstacleIntroducers)
        {
            go.GetComponent<ObstacleIntroducer>().player = this.transform;
        }
    }

    private void Update()
    {
        // For testing Physics!
        /*if (Input.GetKeyDown(KeyCode.Space))
            GetComponent<Rigidbody>().AddForce(Vector3.up * 1000f);*/
    }

    // Testing adding more gravity
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * 9f);
    }

    public void Explode ()
    {
        if (!exploded)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);

            AudioManager.instance.Play("Pop");
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);

            gm.GameOver();

            Debug.Log("Player exploded!");

            exploded = true;
        }
    }

    IEnumerator WaitBeforeShaking (float seconds)
    {
        canShake = false;

        yield return new WaitForSeconds(seconds);

        canShake = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 3 && canShake)
        {
            AudioManager.instance.Play("Impact01");
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);

            StartCoroutine(WaitBeforeShaking(0.1f));
        }
    }
}
