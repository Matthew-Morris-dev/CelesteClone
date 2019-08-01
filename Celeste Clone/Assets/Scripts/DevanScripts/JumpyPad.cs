using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyPad : MonoBehaviour
{
    public float jumpForce = 12;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            AddJumpyPadForce(jumpForce);
        }
    }

    void AddJumpyPadForce(float added_force)
    {
        GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity += Vector2.up * added_force;
    }

}