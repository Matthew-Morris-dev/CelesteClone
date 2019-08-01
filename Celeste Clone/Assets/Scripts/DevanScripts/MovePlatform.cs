using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField]
    private bool PlayerOnPlat;
    [SerializeField]
    private bool travelingToTarget;
    [SerializeField]
    private bool Backatstart;

    public float moveSpeed = 10;

    public Transform startPos;
    public Transform endPos;

    // Start is called before the first frame update
    void Start()
    {
        PlayerOnPlat = false;
        travelingToTarget = false;
        Backatstart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerOnPlat || !Backatstart)
        {
            MovePlat();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerOnPlat = true;
            travelingToTarget = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerOnPlat = false;
    }

    void MovePlat()
    {
        if(travelingToTarget)
        {
            Backatstart = false;
            this.transform.position = Vector2.Lerp(transform.position, endPos.position, moveSpeed / 80);
        }
        else
        {
            this.transform.position = Vector2.Lerp(transform.position, startPos.position, moveSpeed / 130);
        }
        

        if(this.transform.position.x >= (endPos.position.x-2f))
        {
            travelingToTarget = false;
        }
        if (this.transform.position.x <= (startPos.position.x - 2f))
        {
            Backatstart = true;
        }
    }
}
