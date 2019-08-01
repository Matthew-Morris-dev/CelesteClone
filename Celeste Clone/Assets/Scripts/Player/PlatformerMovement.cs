using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CollisionDetection))]
public class PlatformerMovement : MonoBehaviour
{
    [Header("Component Reference")]
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public CollisionDetection coll;
 
    //! [Header("Object Reference")]

    [Header("Control Variables")]
        public float moveSpeed = 5f; 
        public float jumpForce = 3f;
        public float fallMultiplier = 2.5f;
        private bool wallGrab;
        private int wallJumpModifier = 0;
    
        
    // Start is called before the first frame update
    void Start()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!coll) coll = GetComponent<CollisionDetection>();
    }

    // Update is called once per frame
    void Update() {
        wallGrab = coll.onWall && Input.GetButton("Grab");

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Move(x, y);

        if (Input.GetButtonDown("Jump")) {
            if (coll.grounded || coll.onWall)
                Jump(x, y);
        }

        if (!coll.grounded && !Input.GetButton("Jump")) {
            rb.velocity += (Physics2D.gravity * (fallMultiplier - 1) ) * Time.deltaTime;
        }

        if (wallGrab) {
            rb.gravityScale = 0;
        } else {
            rb.gravityScale = 1;
        }

        if (wallJumpModifier != 0) {
            if (coll.grounded) {
                wallJumpModifier = 0;
            }
        }
    }

    private void Move (float x, float y) {
        if (coll.grounded) {
            rb.velocity = new Vector2 (x * moveSpeed, rb.velocity.y);
        } 

        if (wallGrab) {
            rb.velocity = new Vector2 (rb.velocity.x, y * moveSpeed);
        } else if (!coll.onWall) {
            rb.velocity = new Vector2 ( (x+wallJumpModifier) * moveSpeed, rb.velocity.y);
        }
    
    }

    private void Jump (float x, float y) {
        if (x == 0 && !coll.onWall) {
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
        } else if (wallGrab) {
            switch (coll.Wall) {
                case -1: //Wall on the left of the character
                    if (x < 0) {
                        rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
                    } else {
                        rb.velocity = new Vector2 (rb.velocity.x + x * jumpForce, jumpForce);
                    }
                    break;
                case 1: //Wall on the right of the character
                    if (x > 0) {
                        rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
                    } else {
                        rb.velocity = new Vector2 (rb.velocity.x + x * jumpForce, jumpForce);
                    }
                    break;
            }
            
        } else if (coll.onWall) {
                rb.velocity = new Vector2 (rb.velocity.x - jumpForce * coll.Wall, jumpForce);
                wallJumpModifier = -coll.Wall;
        } else {
            rb.velocity = new Vector2 (x * jumpForce, jumpForce);
        }
    }
}
