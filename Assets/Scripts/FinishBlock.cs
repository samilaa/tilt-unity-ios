using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBlock : MonoBehaviour
{
    private GameManager gm;

    public GameObject finishParticles;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        if (other.CompareTag ("Player"))
        {
            gm.FinishLevel();

            Instantiate(finishParticles, transform.position, Quaternion.identity);

            Vector3 winPos = transform.position + Vector3.up * 2f;

            other.GetComponent<Rigidbody>().isKinematic = true;

            Destroy(other.gameObject);
        }
    }

    IEnumerator MoveUp (Vector3 target, Transform objectToMove)
    {
        float t;
        t = 0f;

        while (t < 1.00f)
        {
            t += Time.deltaTime * 0.5f;

            objectToMove.position = Vector3.Lerp(objectToMove.position, target, t);

            yield return null;
        }

        objectToMove.position = target;
    }
}
