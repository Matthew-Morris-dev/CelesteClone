using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public Transform Point1;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Player").GetComponent<Transform>().position = Point1.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        GameObject.Find("Player").GetComponent<Transform>().position = Point1.position;
    }

}
