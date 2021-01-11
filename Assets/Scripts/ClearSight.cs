using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSight : MonoBehaviour
{
    private float DistanceToPlayer;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        DistanceToPlayer = Vector3.Distance(transform.position, player.position);
    }

    void Update()
    {
        if (player != null)
            DistanceToPlayer = Vector3.Distance(transform.position, player.position);
        else
            player = GameObject.FindWithTag("Player")?.transform;

        RaycastHit[] hits;
        // you can also use CapsuleCastAll()
        // TODO: setup your layermask it improve performance and filter your hits.
        hits = Physics.RaycastAll(transform.position, transform.forward, DistanceToPlayer);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("BasicBlock"))
            {
                Renderer R = hit.collider.GetComponent<Renderer>();

                AutoTransparent AT = R.GetComponent<AutoTransparent>();

                if (AT == null) // if no script is attached, attach one
                {
                    AT = R.gameObject.AddComponent<AutoTransparent>();
                }

                AT.BeTransparent(); // get called every frame to reset the falloff
            }

            /*else if (hit.collider.gameObject.CompareTag("BasicBlock"))
            {
                Renderer R = hit.collider.GetComponent<Renderer>();
                Renderer RC = hit.collider.transform.GetChild(0).GetComponent<Renderer>();

                AutoTransparent AT = R.GetComponent<AutoTransparent>();
                AutoTransparent ATC = RC.GetComponent<AutoTransparent>();

                if (AT == null) // if no script is attached, attach one
                {
                    AT = R.gameObject.AddComponent<AutoTransparent>();
                }
                if (ATC == null)
                    ATC = RC.gameObject.AddComponent<AutoTransparent>();

                AT.BeTransparent(); // get called every frame to reset the falloff
                ATC.BeTransparent();
            }*/
        }
    }
}
