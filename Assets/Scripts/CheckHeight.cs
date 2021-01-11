using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHeight : MonoBehaviour
{
    private Player player;

    public float minHeight;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (transform.position.y < minHeight)
            player.Explode();
    }
}
