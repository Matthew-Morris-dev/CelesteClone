using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffMapZones : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>().StartRespawnTransition();
    }
}
