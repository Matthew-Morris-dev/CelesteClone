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
        [SerializeField] private AudioSource JumpSound;
  
    [Header("Object Reference")]
        public SpriteRenderer character;

    [Header("Control Variables")]
        public float moveSpeed = 5f; 
        public float jumpForce = 6f;
        public float fallMultiplier = 2.5f;
        public float jumpDifference = 0.5f;
        public float slideSpeedMultiplier = 0.7f;
        public float WallJumpRecoveryTime = 0.5f;
        public float grabResetTime = 0.4f;
        public float climbingSpeed = 3f;
        public bool facing; //*  True = Sprite Left, false = Sprite Right
        public bool canMove = true;
        private bool wallGrab;
        private float wallJumpModifier = 0;
        private bool canGrab = true;
        private float SlidingJumpMultipier;
        
    // Start is called before the first frame update
    void Start()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!coll) coll = GetComponent<CollisionDetection>();
        if (!animControl) animControl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (canMove)
        {
            wallGrab = coll.onWall && Input.GetButton("Grab") && canGrab;

            animControl.SetBool("ClimbButtonDown", wallGrab);
            animControl.SetBool("Grounded", coll.grounded);
            animControl.SetBool("OnWall", coll.onWall);

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Move(x, y);
            if (x < 0) { facing = true; }
            if (x > 0) { facing = false; }

            if (coll.grounded)
            {
                character.flipX = facing;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (coll.grounded || coll.onWall)
                    Jump(x, y);
            }

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - (jumpDifference + 1)) * Time.deltaTime;
            }

            if (wallGrab)
            {
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = 1;
            }

            if (wallJumpModifier != 0)
            {
                if (coll.grounded)
                {
                    wallJumpModifier = 0;
                }
                else
                {
                    wallJumpModifier = Mathf.Lerp(wallJumpModifier, 0, WallJumpRecoveryTime * Time.deltaTime);
                }
            }

        }
        
    }

    private void Move (float x, float y) {
        if (coll.grounded) {
            rb.velocity = new Vector2 (x * moveSpeed, rb.velocity.y);
            //! Walking animations
            animControl.SetBool("Walking", x != 0);
        } 

        if (wallGrab) { //? Holding the wall
            rb.velocity = new Vector2 (rb.velocity.x, y * climbingSpeed);
            if (y != 0) {
                //! Climbing animation
                animControl.SetBool("Climbing", y != 0);
                animControl.SetFloat("ClimbSpeed", y);
            } else {
                //! Holding animations
                animControl.SetBool("LookingAtWall", Mathf.RoundToInt(x + coll.Wall) != 0);
            }
        } else if (!coll.onWall) { //? Not holding and not near the wall
                rb.velocity = new Vector2 ( (x+wallJumpModifier) * moveSpeed * 0.75f, rb.velocity.y);
        } else { //? Not holding but on the wall 
            if (rb.velocity.y < 0){
                if ( Mathf.RoundToInt(x + coll.Wall) != 0 && Mathf.RoundToInt(x + coll.Wall) != coll.Wall ) {
                    rb.velocity = new Vector2 (rb.velocity.x, slideSpeedMultiplier * rb.velocity.y); //? Sliding
                    //! Sliding down wall animation
                } else {
                    rb.velocity = new Vector2 (x, rb.velocity.y); //? Not Sliding
                    //! Falling animation
                }
            }
        } 
    
    }

    private void Jump (float x, float y) {
        if (!coll.grounded && !coll.onWall) // Jump when in air and not near wall
            return; 

        if (x == 0 && !coll.onWall) { // Jump when grounded and not pressing arrows or near wall
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
            //! Jump animations
            animControl.SetTrigger("Jump");
        } else if (wallGrab) { // Jump while holding the wall
            canGrab = false;
            Invoke("resetGrabStatus", grabResetTime);
            switch (coll.Wall) {
                case -1: //Wall on the left of the character
                    if (x <= 0) {
                        rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
                    } else {
                        rb.velocity = new Vector2 (rb.velocity.x + x * jumpForce, jumpForce);
                    }
                    break;
                case 1: //Wall on the right of the character
                    if (x >= 0) {
                        rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
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

        rb.drag = 4f;
        JumpSound.Play();
    }

    private void resetGrabStatus() {
        canGrab = true;
    }
}
