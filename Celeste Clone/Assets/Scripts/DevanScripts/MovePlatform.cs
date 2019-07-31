using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private bool PlayerOnPlat;

    public float moveSpeed = 10;

    public Transform endPos;

    // Start is called before the first frame update
    void Start()
    {
        PlayerOnPlat = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerOnPlat)
        {
            this.transform.position = Vector2.Lerp(transform.position, endPos.position, moveSpeed/100);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerOnPlat = true;
        }
    }

}
