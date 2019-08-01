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

    [Header("Control Values")]
        public float DashStrength = 10;
        public float ResetTime = 0.25f;
        public float dragRate = 3;
        public float MaxDrag = 12;
        [SerializeField] private bool canDash;
        [SerializeField] private float timeToReset = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!coll) coll = GetComponent<CollisionDetection>();
        if (!movement) movement = GetComponent<PlatformerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (coll.grounded && timeToReset <= 0) {
            canDash = true;
            movement.enabled = true;
        } else if (timeToReset > 0) {
            timeToReset -= Time.deltaTime;
        } else {
            movement.enabled = true;
        }

        rb.drag = Mathf.Lerp (rb.drag, 0, dragRate * Time.deltaTime);

        if (coll.onWall) {
            movement.enabled = true;
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

            rb.velocity = new Vector2 (x * DashStrength, y  * DashStrength);
            rb.drag = MaxDrag;
            movement.enabled = false;
        }
    }
}
