using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffMapZones : MonoBehaviour
{   
    private AudioSource RespawnSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            RespawnPlayer();
            if (!RespawnSound) {
                RespawnSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
            }
            RespawnSound.Play();
        }
    }

    private void RespawnPlayer()
    {
        GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>().StartRespawnTransition();
    }
}
