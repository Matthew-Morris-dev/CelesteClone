using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpawnPoint1 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>().curr_level = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>().curr_level = 1;
    }

}
