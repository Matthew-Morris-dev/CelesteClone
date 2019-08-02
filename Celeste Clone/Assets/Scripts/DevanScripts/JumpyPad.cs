using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyPad : MonoBehaviour
{
    public float padForce = 12;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {

                if (hitPos.normal.y >= -1)
                {
                    AddJumpyPadForce(padForce, collision.gameObject);
                }
            }
        }
    }

    void AddJumpyPadForce(float added_force, GameObject player)
    {
        player.GetComponent<Rigidbody2D>().velocity += Vector2.up * added_force;
    }

}