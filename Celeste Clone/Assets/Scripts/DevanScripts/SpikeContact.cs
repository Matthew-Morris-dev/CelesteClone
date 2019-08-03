using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeContact : MonoBehaviour
{

    private AudioSource DeathSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Player"))
        {
            foreach(ContactPoint2D hitPos in collision.contacts)
            {
                if(hitPos.normal.y == -1)
                {
                    GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>().StartRespawnTransition();
                }
            }

            if (!DeathSound) {
                DeathSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
            }

            DeathSound.Play();
        }
    }
}
