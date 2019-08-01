using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyPad : MonoBehaviour
{
    public float jumpForce = 12;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddJumpyPadForce(jumpForce, collision.gameObject);
        }
    }

    void AddJumpyPadForce(float added_force, GameObject player)
    {
        player.GetComponent<Rigidbody2D>().velocity += Vector2.up * added_force;
    }

}