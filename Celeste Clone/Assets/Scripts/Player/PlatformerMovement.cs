using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CollisionDetection))]
[RequireComponent(typeof(Animator))]
public class PlatformerMovement : MonoBehaviour
{
    [Header("Component Reference")]
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public CollisionDetection coll;
        [HideInInspector] public Animator animControl;
 
    [Header("Object Reference")]
        public SpriteRenderer character;

    [Header("Control Variables")]
        public float moveSpeed = 5f; 
        public float jumpForce = 3f;
        public float fallMultiplier = 2.5f;
        public float jumpDifference = 0.5f;
        public float slideSpeedMultiplier = 0.7f;
        public float grabResetTime = 0.4f;
        public bool facing; //*  True = Sprite Left, false = Sprite Right
        private bool wallGrab;
        private int wallJumpModifier = 0;
        private bool canGrab = true;
        
    // Start is called before the first frame update
    void Start()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!coll) coll = GetComponent<CollisionDetection>();
        if (!animControl) animControl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        
        wallGrab = coll.onWall && Input.GetButton("Grab") && canGrab;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Move(x, y);
        if (x < 0) {facing = true;} 
        if (x > 0) {facing = false;}
        
        character.flipX = facing;

        if (Input.GetButtonDown("Jump")) {
            if (coll.grounded || coll.onWall)
                Jump(x, y);
        }

        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;  
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - (jumpDifference + 1) ) * Time.deltaTime; 
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

        animControl.SetFloat("Motion_X", Mathf.Abs(rb.velocity.x));
        //TODO animControl.SetFloat("Motion_Y", rb.velocity.y);
    }

    private void Move (float x, float y) {
        if (coll.grounded) {
            rb.velocity = new Vector2 (x * moveSpeed, rb.velocity.y);
        } 

        if (wallGrab) { //? Holding the wall
            rb.velocity = new Vector2 (rb.velocity.x, y * moveSpeed);
        } else if (!coll.onWall) { //? Not holding and not near the wall
            rb.velocity = new Vector2 ( (x+wallJumpModifier) * moveSpeed, rb.velocity.y);
        } else { //? Not holding but on the wall 
            if (rb.velocity.y < 0){
                if ( Mathf.RoundToInt(x + coll.Wall) != 0 && Mathf.RoundToInt(x + coll.Wall) != coll.Wall ) {
                    rb.velocity = new Vector2 (rb.velocity.x, slideSpeedMultiplier * rb.velocity.y); //? Sliding
                    Debug.Log("Sliding");
                } else {
                    rb.velocity = new Vector2 (x, rb.velocity.y); //? Not Sliding
                    Debug.Log("Not Sliding");
                }
            }
        } 
    
    }

    private void Jump (float x, float y) {
        if (!coll.grounded && !coll.onWall) // Jump when in air and not near wall
            return; 

        if (x == 0 && !coll.onWall) { // Jump when grounded and not pressing arrows or near wall
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
        } else if (wallGrab) { // Jump while holding the wall
            canGrab = false;
            Invoke("resetGrabStatus", grabResetTime);
            switch (coll.Wall) {
                case -1: //Wall on the left of the character
                    if (x <= 0) {
                        rb.velocity = new Vector2 (rb.velocity.x, 2 * jumpForce);
                    } else {
                        rb.velocity = new Vector2 (rb.velocity.x + x * jumpForce, jumpForce);
                    }
                    break;
                case 1: //Wall on the right of the character
                    if (x >= 0) {
                        rb.velocity = new Vector2 (rb.velocity.x, 2 * jumpForce);
                    } else {
                        rb.velocity = new Vector2 (rb.velocity.x + x * jumpForce, jumpForce);
                    }
                    break;
            }
            
        } else if (coll.onWall) { //Jump while on wall but not holding
                rb.velocity = new Vector2 (rb.velocity.x - jumpForce * coll.Wall, jumpForce);
                wallJumpModifier = -coll.Wall;
        } else { // Jump while grounded and pressing arrows
            rb.velocity = new Vector2 (x * jumpForce, jumpForce);
        }
    }

    private void resetGrabStatus() {
        canGrab = true;
    }
}
