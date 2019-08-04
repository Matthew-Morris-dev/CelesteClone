using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CollisionDetection))]
[RequireComponent(typeof(PlatformerMovement))]

public class Dash : MonoBehaviour
{

    [Header("Component Reference")]
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public CollisionDetection coll;
        [HideInInspector] public PlatformerMovement movement;
        [HideInInspector] public Animator animControl;
        [SerializeField] private AudioSource SoundEffect;

    [Header("Object Reference")]
        public SpeedClones Clone1;    
        public SpeedClones Clone2;

        public Color visible;
        public Color invisible;

    [Header("Control Values")]
        public float DashStrength = 25;
        public float ResetTime = 0.15f;
        public float dragRate = 3;
        public float MaxDrag = 15;

        [Space]

        public float cloneOneDistance = 0.2f;
        public float cloneTwoDistance = 0.5f;
        [SerializeField] private bool canDash;
        [SerializeField] private float timeToReset = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!coll) coll = GetComponent<CollisionDetection>();
        if (!movement) movement = GetComponent<PlatformerMovement>();
        if (!animControl) animControl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int x = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
        int y = Mathf.RoundToInt(Input.GetAxis("Vertical"));

        if (coll.grounded && timeToReset <= 0) {
            canDash = true;
            movement.canMove = true;

        } else if (timeToReset > 0) {
            timeToReset -= Time.deltaTime;
            
        } else {
            movement.canMove = true;
        }
        
        rb.drag = Mathf.Lerp (rb.drag, 0, dragRate * Time.deltaTime);
        
        if (coll.onWall) {
            movement.canMove = true;
        }
        
        if (canDash && Input.GetButtonDown("Dash")) {

            canDash = false;
            if (y == 0) {
                if (movement.facing) {
                    x = x == 0 ? -1 : x;
                } else {
                    x = x == 0 ?  1 : x;
                }
            }

            timeToReset = ResetTime;
            animControl.SetTrigger("Dash");
            SoundEffect.Play();
            rb.velocity = new Vector2 (x * DashStrength, y  * DashStrength);
            rb.drag = MaxDrag;
            movement.canMove = false;
            Invoke("CloneOne", cloneOneDistance);
            Invoke("CloneTwo", cloneTwoDistance);
        }
    }

    private void CloneOne () {
        Clone1.SetPosition(transform.position, rb.velocity.x < 0);
    }

    private void CloneTwo() {
        Clone2.SetPosition(transform.position, rb.velocity.x < 0);
    }
}
