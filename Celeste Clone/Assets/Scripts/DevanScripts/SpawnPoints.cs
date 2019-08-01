using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public List<Transform> targets;
    public float moveSpeed;

    public Transform Point0;
    public Transform Point1;
    public Transform Point2;

    public int curr_level;
    
    void Start()
    {
        curr_level = 0;
        GameObject.Find("Player").GetComponent<Transform>().position = Point0.position;
    }

    void Update()
    {
        if (curr_level == 1)
        {
            GameObject.Find("Main Camera").GetComponent<Transform>().position = Vector3.Lerp(GameObject.Find("Main Camera").GetComponent<Transform>().position, targets[0].position, moveSpeed / 80);
        }
        if (curr_level == 2)
        {
            GameObject.Find("Main Camera").GetComponent<Transform>().position = Vector3.Lerp(GameObject.Find("Main Camera").GetComponent<Transform>().position, targets[1].position, moveSpeed / 80);
        }
    }

    public void RespawnPlayer()
    {
        switch(curr_level)
        {
            case 0:
                GameObject.Find("Player").GetComponent<Transform>().position = Point0.position;
                break;
            case 1:
                GameObject.Find("Player").GetComponent<Transform>().position = Point1.position;
                break;
            case 2:
                GameObject.Find("Player").GetComponent<Transform>().position = Point2.position;
                break;
        }
    }

}
