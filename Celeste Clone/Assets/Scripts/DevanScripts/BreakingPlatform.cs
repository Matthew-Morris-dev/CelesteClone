using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    Rigidbody2D rd_breakplat;
    public Animator animator;

    [SerializeField]
    private float time_to_break = 0.5f;
    [SerializeField]
    private float time_to_comeback = 2f;

    public BreakingPlatformsManager BPM;
    void Start()
    {
        rd_breakplat = GetComponent<Rigidbody2D>();
        BPM = FindObjectOfType<BreakingPlatformsManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Player"))
        {
            Debug.Log(this.gameObject.name);
            if (this.gameObject.name == "BreakingPlat")
            {
                Debug.LogWarning("we enter here");
                BPM.DelayedDisable1();
            }
            else if (this.gameObject.name == "BreakingPlat (1)")
            {
                BPM.DelayedDisable2();
            }
            else
            {
                BPM.DelayedDisable3();
            }
            //Invoke("BreakPlatform", time_to_break);
            //Invoke("BringBackPlat", time_to_comeback);
        }
    }

    void BreakPlatform()
    {
        Debug.LogError("we enter here");
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("isActive", false);
        Invoke("BringBackPlat", time_to_comeback);
    }

    void BringBackPlat()
    {
        Debug.LogError("we enter here 2");
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        animator.SetBool("isActive", true);
    }
}
