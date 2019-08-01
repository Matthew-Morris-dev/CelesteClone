using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public Transform Point1;
    
    void Start()
    {
        GameObject.Find("Player").GetComponent<Transform>().position = Point1.position;
    }

    public void RespawnPlayer()
    {
        GameObject.Find("Player").GetComponent<Transform>().position = Point1.position;
    }

}
