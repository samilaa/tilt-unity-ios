using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void PickUp (GameObject player)
    {
        player.GetComponent<Player>().hasKey = true;

        AudioManager.instance.Play("Clink");
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            PickUp(other.gameObject);
        }
    }
}
