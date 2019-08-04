using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatformsManager : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;
    public float spawnDelay;
    public float breakDelay;
    public float timer1;
    public float timer2;
    public float timer3;
    public bool enabled1 = true;
    public bool enabled2 = true;
    public bool enabled3 = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled1 && (Time.time >= timer1))
        {
            Enable1();

        }
        if (!enabled2 && (Time.time >= timer2))
        {
            Enable2();
        }
        if (!enabled3 && (Time.time >= timer3))
        {
            Enable3();

        }
    }

    public void DelayedDisable1()
    {
        Invoke("Disable1", breakDelay);
    }

    public void DelayedDisable2()
    {
        Invoke("Disable2", breakDelay);
    }

    public void DelayedDisable3()
    {
        Invoke("Disable3", breakDelay);
    }

    public void Disable1()
    {
        platform1.SetActive(false);
        enabled1 = false;
        timer1 = Time.time + spawnDelay;
    }

    public void Disable2()
    {
        platform2.SetActive(false);
        enabled2 = false;
        timer2 = Time.time + spawnDelay;
    }

    public void Disable3()
    {
        platform3.SetActive(false);
        enabled3 = false;
        timer3 = Time.time + spawnDelay;
    }

    public void Enable1()
    {
        platform1.SetActive(true);
        enabled1 = true;
    }

    public void Enable2()
    {
        platform2.SetActive(true);
        enabled2 = true;
    }

    public void Enable3()
    {
        platform3.SetActive(true);
        enabled3 = true;
    }
}
