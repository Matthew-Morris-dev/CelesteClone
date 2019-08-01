using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraOnLevelChange : MonoBehaviour
{
    public List<Transform> targets;
    public float moveSpeed;

    public int currentlevel;

    void Start()
    {
        currentlevel = 0;
    }
    
    void Update()
    {
        if(currentlevel == 1)
        {
            GameObject.Find("Main Camera").GetComponent<Transform>().position = Vector3.Lerp(GameObject.Find("Main Camera").GetComponent<Transform>().position, targets[0].position, moveSpeed / 80);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentlevel = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentlevel = 1;
    }

}
