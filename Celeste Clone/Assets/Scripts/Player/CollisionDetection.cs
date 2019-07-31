using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [Header("Results")]
        public bool grounded;
        public bool onWall;

    [Header("Collision Controllers")]
        public LayerMask CollisionLayer; 
        [Range(0,1f)] public float collisionRadius = 0.25f;
        public Vector2 bottomOffset, rightOffset, leftOffset;


    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, CollisionLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, CollisionLayer) || 
                 Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, CollisionLayer);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);

    }
}
