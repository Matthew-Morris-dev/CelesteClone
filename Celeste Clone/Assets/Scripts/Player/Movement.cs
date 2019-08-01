using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CREDIT WHERE CREDIT IS DUE

    The code written in this file was created with the asistance of a tutorial 
    made by Mix and Jam. The video is available on YouTube. 

    Mix and Jam - Celeste Movement: https://www.youtube.com/watch?v=STyY26a_dPY&t=6s

    in conjunction with Board to Bits Games tutorial on better 2D jumping

    Board to Bits Games - Better Jumping in Unity: https://www.youtube.com/watch?v=7KiK0Aqtmzc 

 */

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CollisionDetection))]
public class Movement : MonoBehaviour
{
    [Header("Component Reference")] //? References components of this gameobject
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CollisionDetection collisionDetection;

    [Header("Object Reference")] //? References other gameobjects or components of other gameobjects
        [SerializeField] private GameObject main_Camera; 

    [Header("Control Values")] //? Variables that can be used to control and fine tune the movement
        public float moveSpeed;
        public float jumpForce;
        public float slideSpeed = 2f;
        public float fallMultiplier = 2.5f; //* Increased gravity when falling makes jump better

    [Header("Private")]
        private bool wallclimb;
        private bool canMove;
    


    // Start is called before the first frame update
    void Start()
    {
        if (!rb) 
            rb = GetComponent<Rigidbody2D>();
        if (!main_Camera) 
            main_Camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (!collisionDetection)
            collisionDetection = GetComponent<CollisionDetection>();


    }

    // Update is called once per frame
    void Update()
    {
        wallclimb = collisionDetection.onWall && Input.GetButton("Grab");

        float x_direction = Input.GetAxis("Horizontal"); // Left and right movement
        float y_direction = Input.GetAxis("Vertical"); // Will need this later but for now its not being used

        Vector2 move_direction = new Vector2 (x_direction * moveSpeed, rb.velocity.y);

        Move(move_direction);

        if (Input.GetButtonDown("Jump")) {
            if (collisionDetection.grounded){
                Jump();
            } else if (collisionDetection.onWall) {
                WallJump();
            }
        }

        if (wallclimb) {
            rb.velocity = new Vector2(rb.velocity.x, y_direction * moveSpeed);
        }

        if (collisionDetection.onWall && !collisionDetection.grounded) {
            if (x_direction != 0 && !wallclimb)
                WallSlide();
        }

        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;  
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1.5f) * Time.deltaTime; 
        }

    }

    private void WallSlide() {
        if (!canMove)
            return;
        rb.velocity = new Vector2 (rb.velocity.x, -slideSpeed);
    }

    private void Jump () {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
    }

    private void WallJump(){
        rb.velocity += Vector2.one * jumpForce * -collisionDetection.Wall;

    }

    private void Move(Vector2 direction){
        //? Splitting into a function to make it easier to find and change 

        if (!collisionDetection.grounded && direction.x == 0) {
            return;
        } else {
            if (collisionDetection.Wall == 0)
                rb.velocity = direction;

            if (collisionDetection.Wall == 1 && collisionDetection.grounded && direction.x < 0) 
                rb.velocity = direction;
            
            if (collisionDetection.Wall == -1 && collisionDetection.grounded && direction.x > 0) 
                rb.velocity = direction;
        }
    }
}
