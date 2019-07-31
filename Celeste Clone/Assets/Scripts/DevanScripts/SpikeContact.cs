using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeContact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Player"))
        {
            foreach(ContactPoint2D hitPos in collision.contacts)
            {
                if(hitPos.normal.y == -1)
                {
                    GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>().RespawnPlayer();
                }
            }
        }
    }
}
