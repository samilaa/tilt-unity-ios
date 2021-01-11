using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float force = 10f;

    public float resetTime = 1f;

    public bool ready;

    private void Start()
    {
        ready = true;
    }

    void JumpObject (Rigidbody rb)
    {
        if (ready)
        {
            // Testing removing velocity before launching
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * force);

            GetComponent<Animator>()?.SetTrigger("Activate");

            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
            AudioManager.instance.Play("Doink");
            AudioManager.instance.Play("Pop");

            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset ()
    {
        ready = false;

        yield return new WaitForSeconds(resetTime);

        ready = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            JumpObject(other.GetComponent<Rigidbody>());

            Debug.Log("Trampoline activated!");
        }
    }
}
